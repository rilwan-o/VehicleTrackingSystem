using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehicleLocationDto
    {
        public LatLonPoint LatLonPoint { get; set; }
        public string TrackingId { get; set; }
    }
}
