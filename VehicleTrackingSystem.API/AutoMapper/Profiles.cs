using AutoMapper;
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
