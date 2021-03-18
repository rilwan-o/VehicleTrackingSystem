using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly AppSettings _appSettings;

        public VehicleRepository(ApplicationDbContext dbContext, IOptions<AppSettings> appSettings)
        {
            _dbContext = dbContext;
            _appSettings = appSettings.Value;
        }

        public async Task<bool> VehicleExists(string brand, string chasis)
        {
            var existingVehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.ChasisNumber == chasis && v.Brand ==brand && v.Status == _appSettings.VehicleSettings.ActiveStatus);
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
