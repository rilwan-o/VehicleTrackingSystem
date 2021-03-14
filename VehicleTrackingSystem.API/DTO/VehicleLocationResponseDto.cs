using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehicleLocationResponseDto
    {
        public LatLonPoint LocationCordinate { get; set; }
        public string LocationName { get; set; }
    }
}
