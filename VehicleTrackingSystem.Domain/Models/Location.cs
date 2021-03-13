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
        public string LocationCordinate { get; set; }
        public string LocationName { get; set; }
        public DateTime DateTime { get; set; }
        public int VehicleId { get; set; }
    }
}
