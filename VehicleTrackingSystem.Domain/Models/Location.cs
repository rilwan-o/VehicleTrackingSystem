using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
