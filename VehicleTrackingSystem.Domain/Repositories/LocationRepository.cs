using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public LocationRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddVehiclePosition(Location location)
        {
            await _dbContext.Locations.AddAsync(location);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Location> GetVehicleLocation(int vehicleId)
        {
            return await _dbContext.Locations.Where(v => v.VehicleId == vehicleId)
                                    .OrderByDescending(v => v.DateTime).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Location>> GetVehicleLocations(int vehicleId, DateTime From, DateTime To)
        {

            var locations = await _dbContext.Locations.Where(v => (v.VehicleId == vehicleId) && (v.DateTime >= From) && (v.DateTime <= To))
                                               .OrderByDescending(v => v.DateTime).ToListAsync();

            return locations;
        }
    }
}
