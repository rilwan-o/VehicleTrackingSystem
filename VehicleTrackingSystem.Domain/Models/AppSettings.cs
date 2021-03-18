using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.Domain.Models
{
    public class AppSettings
    {
        public JWT JWT { get; set; }
        public VehicleSettings VehicleSettings { get; set; }
        public string DatabaseSecureLogGroups { get; set; }
    }

    public class JWT
    {
        public string ExpiryInMinutes { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public string Secret { get; set; }
    }

    public class VehicleSettings
    {
        public string ActiveStatus { get; set; }
        public string GoogleMapUrl { get; set; }
        public string Key { get; set; }
    }
}
