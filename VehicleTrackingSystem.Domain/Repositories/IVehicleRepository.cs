using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public interface IVehicleRepository
    {
        Task<string> AddVehicle(Vehicle vehicle);
        Task<Vehicle> GetVehicleByTrackingId(string trackingId);
        Task <bool> VehicleExists(string brand, string chasis);
     
    }
}
