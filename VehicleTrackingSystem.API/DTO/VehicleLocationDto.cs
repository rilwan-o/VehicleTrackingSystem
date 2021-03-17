﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.DTO
{
    public class VehicleLocationDto
    {
        public LatLonPoint LatLonPoint { get; set; }
        public string TrackingId { get; set; }
    }
}