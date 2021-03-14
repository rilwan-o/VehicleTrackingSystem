using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.Services
{
    public interface IVehicleTractingService
    {
        Task<bool> IsVehicleNew(string chasis);
        Task RegisterVehicle(VehicleRegisterDto vehicle, string id);
        Task AddVehiclePosition(VehicleLocationDto location, int id);
        Task<VehicleLocationResponseDto> GetVehicleLocation(int vehicleId);
        Task<IEnumerable<Location>> GetVehicleLocations(int vehicleId, DateTime From, DateTime To);
        Task <Vehicle> GetVehicleByTrackingId(string trackingId);
    }
}
