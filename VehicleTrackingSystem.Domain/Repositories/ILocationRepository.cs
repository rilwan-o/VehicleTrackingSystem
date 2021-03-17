using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public interface ILocationRepository
    {
        Task AddVehiclePosition(Location location);
        Task<Location> GetVehicleLocation(int vehicleId);
        Task<IEnumerable<Location>> GetVehicleLocations(int vehicleId, DateTime From, DateTime To);
    }
}
