using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehicleResponseDto
    { 
        public string TrackingId { get; set; }
        public string Brand { get; set; }
        public string ChasisNumber { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }    
        public DateTime DateRegistered { get; set; }
       
    }
}
