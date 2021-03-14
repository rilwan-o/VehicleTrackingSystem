using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.AutoMapper
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<VehicleRegisterDto, Vehicle>();
            CreateMap<Vehicle, VehicleResponseDto>();
        }
    }
}
