using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehiclePositionsDto
    {
        public string TrackingId { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
