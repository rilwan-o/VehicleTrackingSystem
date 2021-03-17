using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleTrackingSystem.API.Enumerations
{
    public enum ResponseEnum
    {
        [EnumDisplay(Name = "Approved Or Completed Successfully", Description = "Operation was successful")]
        ApprovedOrCompletedSuccesfully = 00,

        [EnumDisplay(Name = "Duplicate Vehicle", Description = "Vehicle already exists")]
        DuplicateVehicle = 01,

        [EnumDisplay(Name = "Invalid vehicle Id", Description = "Invalid vehicle Id")]
        InvalidVehicleId = 02,
        [EnumDisplay(Name = "System Malfunction ", Description = "System malfunction ")]
        SystemMalfunction = 96,
    }
}
