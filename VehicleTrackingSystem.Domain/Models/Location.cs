using System;

namespace VehicleTrackingSystem.Domain.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string LocationCordinates { get; set; }
        public DateTime DateTime { get; set; }
        public int VehicleId { get; set; }
    }
}
