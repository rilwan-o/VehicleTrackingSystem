using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.API.DTO
{
    public class ApiResponseData : ApiResponse
    {
        public object Data { get; set; }
    }
}
