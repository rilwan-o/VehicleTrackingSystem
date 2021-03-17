using System;

namespace VehicleTrackingSystem.Domain.DTO
{
    public class VehiclePositionsDto
    {
        public string TrackingId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
