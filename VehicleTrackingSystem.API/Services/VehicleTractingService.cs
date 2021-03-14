using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.Domain.Models;
using VehicleTrackingSystem.Domain.Repositories;

namespace VehicleTrackingSystem.API.Services
{
    public class VehicleTractingService : IVehicleTractingService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public VehicleTractingService(IVehicleRepository vehicleRepository, ILocationRepository locationRepository, IMapper mapper, IConfiguration configuration) 
        { 
            _vehicleRepository = vehicleRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task AddVehiclePosition(VehicleLocationDto vehicleLocation, int id)
        {

            var location = new Location
            {
                LocationCordinate = JsonSerializer.Serialize(vehicleLocation.LatLonPoint),
                DateTime = DateTime.Now,
                VehicleId = id
            };
           await _locationRepository.AddVehiclePosition(location);
        }

        public async Task<Vehicle> GetVehicleByTrackingId(string trackingId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByTrackingId(trackingId);
            return vehicle;
        }

        public async Task<VehicleLocationResponseDto> GetVehicleLocation(int vehicleId)
        {
            var location =  await _locationRepository.GetVehicleLocation(vehicleId);
            var locationDto = _mapper.Map<Location, VehicleLocationResponseDto>(location);
            return locationDto;
        }

        public async Task<IEnumerable<Location>> GetVehicleLocations(int vehicleId, DateTime From, DateTime To)
        {
            return await _locationRepository.GetVehicleLocations(vehicleId, From, To);
        }

        public async Task RegisterVehicle(VehicleRegisterDto vehicleDto, string id)
        {
            var vehicle = _mapper.Map<VehicleRegisterDto, Vehicle>(vehicleDto);
            vehicle.UserId = id;
            vehicle.TrackingId = Guid.NewGuid().ToString();
            vehicle.Status = _configuration["Vehicle:Status"];
            vehicle.DateRegistered = DateTime.Now;

            await _vehicleRepository.AddVehicle(vehicle);
        }

        public async Task<bool> IsVehicleNew(string chasis)
        {

            return await _vehicleRepository.VehicleExists(chasis);
        }
    }
}
