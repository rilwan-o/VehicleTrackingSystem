﻿using System;

namespace VehicleTrackingSystem.Domain.DTO
{
    public class VehicleRegisterDto
    {
        public string Brand { get; set; }
        public string ChasisNumber { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public DateTime Year { get; set; }
    }
}
