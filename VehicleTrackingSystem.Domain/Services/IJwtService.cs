using System.Collections.Generic;
using System.Security.Claims;

namespace VehicleTrackingSystem.Domain.Services
{
    public interface IJwtService
    {
        TokenData GenerateSecurityToken(List<Claim> authClaims);
    }
}
