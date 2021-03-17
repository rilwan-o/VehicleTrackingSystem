using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.API.Services;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IVehicleTrackingService _vehicleTrackingService;

        public VehiclesController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, IVehicleTrackingService vehicleTrackingService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _vehicleTrackingService = vehicleTrackingService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [Route("register-vehicle")]
        public async Task<IActionResult> RegisterVehicle([FromBody] VehicleRegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);

            var vehicleIsNew = await _vehicleTrackingService.IsVehicleNew(model.Brand, model.ChasisNumber);
            if (vehicleIsNew)
            {
                var trackingId = await _vehicleTrackingService.RegisterVehicle(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return CreatedAtRoute("vehicle", new { trackid = trackingId }, null);
            }

            return BadRequest(new ApiResponse { Code = "02", Description = "Vehicle already exists" });

        }

        [HttpGet("{trackid}", Name = "vehicle")]
        public async Task<IActionResult> Vehicle(string trackid)
        {
            if (!ModelState.IsValid) return BadRequest(trackid);
            var vehicle = await _vehicleTrackingService.GetVehicleDtoByTrackingId(trackid.Trim());
            return Ok(vehicle);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("record-vehicle-position")]
        public async Task<IActionResult> AddVehiclePosition([FromBody] VehicleLocationDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var vehicle = await _vehicleTrackingService.GetVehicleByTrackingId(model.TrackingId);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId != vehicle.UserId) return StatusCode(403); 
            if (vehicle == null) return BadRequest(new ApiResponse { Code = "02", Description = "Invalid vehicle id" });

            await _vehicleTrackingService.AddVehiclePosition(model, vehicle.Id);
            return Ok();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("get-current-vehicle-position/{trackingId}")]
        public async Task<IActionResult> GetCurrentVehiclePosition(string trackingId)
        {
            if (!ModelState.IsValid) return BadRequest(trackingId);
            var vehicle = await _vehicleTrackingService.GetVehicleByTrackingId(trackingId.Trim());

            if (vehicle == null) return BadRequest(new ApiResponse { Code = "02", Description = "Invalid vehicle id" });

            var location = await _vehicleTrackingService.GetVehicleLocation(vehicle.Id);
            return Ok(location);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("get-vehicle-positions")]
        public async Task<IActionResult> GetVehiclePositions([FromBody]VehiclePositionsDto model)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var vehicle = await _vehicleTrackingService.GetVehicleByTrackingId(model.TrackingId.Trim());

            if (vehicle == null) return BadRequest(new ApiResponse { Code = "02", Description = "Invalid vehicle id" });

            var location = await _vehicleTrackingService.GetVehicleLocations(vehicle.Id, model.From, model.To);
            return Ok(location);
        }






    }
}
