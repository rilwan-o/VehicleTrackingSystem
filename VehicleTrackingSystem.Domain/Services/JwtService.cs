using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Services
{
    public class JwtService : IJwtService
    {
        private readonly AppSettings _appSettings;
        public JwtService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public TokenData GenerateSecurityToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT.Secret));
            int expiryInMinutes = Convert.ToInt32(_appSettings.JWT.ExpiryInMinutes);
            var token = new JwtSecurityToken(
                issuer: _appSettings.JWT.ValidIssuer,
                audience: _appSettings.JWT.ValidAudience,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            var tokenData = new TokenData
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };

            return tokenData;
        }
    }

    public class TokenData
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }

}
