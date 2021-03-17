using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.DTO;
using VehicleTrackingSystem.Domain.Enumerations;
using VehicleTrackingSystem.Domain.Models;
using VehicleTrackingSystem.Domain.Services;

namespace VehicleTrackingSystem.API.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
                var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = ResponseEnum.DuplicateUser.ResponseCode(),
                    Description = ResponseEnum.DuplicateUser.DisplayName()
                });
            }
               
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse
                {
                    Code = ResponseEnum.UserCreationFailed.ResponseCode(),
                    Description = ResponseEnum.UserCreationFailed.DisplayName()
                });
            }
                

            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return Ok(new ApiResponse { Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(), 
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName() 
            });
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                return Ok(_jwtService.GenerateSecurityToken(authClaims));
   
            }
            return BadRequest(new ApiResponse {Code=ResponseEnum.UserLoginFailed.ResponseCode(),
                Description= ResponseEnum.UserLoginFailed.DisplayName()});
        }

       
        [HttpPost]
        [Route("RegisterAdmin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return BadRequest(new ApiResponse{Code = ResponseEnum.DuplicateUser.ResponseCode(), Description = ResponseEnum.DuplicateUser.DisplayName() });

            }

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(new ApiResponse { Code = ResponseEnum.UserCreationFailed.ResponseCode(), 
                    Description = ResponseEnum.UserCreationFailed.DisplayName() });

            }

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }

            return Ok(new ApiResponse { Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(), 
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName() });
        }
    }
}
