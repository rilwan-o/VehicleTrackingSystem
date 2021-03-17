using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public VehicleRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> VehicleExists(string brand, string chasis)
        {
            var existingVehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.ChasisNumber == chasis && v.Brand ==brand && v.Status == _configuration["Vehicle:Status"]);
            return existingVehicle == null;
        }
        public async Task<Vehicle> GetVehicleByTrackingId(string trackingId)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.TrackingId == trackingId);
            return vehicle;
        }
        public async Task<string> AddVehicle(Vehicle vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
            return vehicle.TrackingId;
        }
    }
}
