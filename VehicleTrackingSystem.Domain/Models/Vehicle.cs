using System;

namespace VehicleTrackingSystem.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string TrackingId { get; set; }
        public string Brand { get; set; }
        public string ChasisNumber { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
        public string Status { get; set; }
        public DateTime DateRegistered { get; set; }
        public DateTime DateUnRegistered { get; set; }
        public DateTime DateUpdated{ get; set; }

    }
}
