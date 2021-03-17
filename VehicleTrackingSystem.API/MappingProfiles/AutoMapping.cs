using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.DTO;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.MappingProfiles
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<VehicleRegisterDto, Vehicle>();
            CreateMap<Vehicle, VehicleResponseDto>();
        }
    }
}
