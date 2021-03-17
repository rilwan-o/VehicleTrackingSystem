using System;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehicleLocationResponseDto
    {
        public LatLonPoint LocationCordinate { get; set; }
        public string LocationName { get; set; }
        public DateTime Datetime { get; set; }
    }
}
