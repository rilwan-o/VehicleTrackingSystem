using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.Domain.Models;
using VehicleTrackingSystem.Domain.Repositories;

namespace VehicleTrackingSystem.API.Services
{
    public class VehicleTrackingService : IVehicleTrackingService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILocationRepository _locationRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IHttpClientFactory _clientFactory;
        public VehicleTrackingService(IVehicleRepository vehicleRepository, ILocationRepository locationRepository, IMapper mapper, IConfiguration configuration, IHttpClientFactory clientFactory) 
        { 
            _vehicleRepository = vehicleRepository;
            _locationRepository = locationRepository;
            _mapper = mapper;
            _configuration = configuration;
            _clientFactory = clientFactory;
        }
        public async Task AddVehiclePosition(VehicleLocationDto vehicleLocation, int id)
        {

            var location = new Location
            {
                LocationCordinates = JsonConvert.SerializeObject(vehicleLocation.LatLonPoint),
                DateTime = DateTime.Now,
                VehicleId = id
            };
           await _locationRepository.AddVehiclePosition(location);
        }

        public async Task<VehicleResponseDto> GetVehicleDtoByTrackingId(string trackingId)
        {
            var vehicle = await GetVehicleByTrackingId(trackingId);
            var vehicleResponseDto = _mapper.Map<Vehicle, VehicleResponseDto>(vehicle);
            return vehicleResponseDto;
        }
        public async Task<Vehicle> GetVehicleByTrackingId(string trackingId)
        {
            var vehicle = await _vehicleRepository.GetVehicleByTrackingId(trackingId);           
            return vehicle;
        }
        public async Task<VehicleLocationResponseDto> GetVehicleLocation(int vehicleId)
        {
            var location =  await _locationRepository.GetVehicleLocation(vehicleId);
            var latlong = JsonConvert.DeserializeObject<LatLonPoint>(location.LocationCordinates);
            var locationDto = new VehicleLocationResponseDto
            {
                LocationCordinate = latlong,
                Datetime = location.DateTime,
                LocationName = await GetLocationName(latlong)
            };

            return locationDto;
        }

        public async Task<IEnumerable<VehicleLocationResponseDto>> GetVehicleLocations(int vehicleId, DateTime From, DateTime To)
        {
            List<VehicleLocationResponseDto> vehicleLocationResponseDtos = new List<VehicleLocationResponseDto>();
            var locations =  await _locationRepository.GetVehicleLocations(vehicleId, From, To);
            Parallel.ForEach(locations, async location =>
            {
                var vlocationDto = await GetVehicleLocation(location.VehicleId);
                vehicleLocationResponseDtos.Add(vlocationDto);
            });

            return vehicleLocationResponseDtos;
        }

        public async Task<string> RegisterVehicle(VehicleRegisterDto vehicleDto, string id)
        {
            var vehicle = _mapper.Map<VehicleRegisterDto, Vehicle>(vehicleDto);
            vehicle.UserId = id;
            vehicle.TrackingId = Guid.NewGuid().ToString();
            vehicle.Status = _configuration["Vehicle:Status"];
            vehicle.DateRegistered = DateTime.Now;

            return await _vehicleRepository.AddVehicle(vehicle);
        }

        public async Task<bool> IsVehicleNew(string brand, string chasis)
        {
            return await _vehicleRepository.VehicleExists(brand, chasis);
        }

        private async Task<string> GetLocationName(LatLonPoint latlong)
        {
            string name;
            var url = $"{_configuration["Vehicle:GoogleMapUrl"]}latlng={latlong.Latitude},{latlong.Longitiude}&key={_configuration["Vehicle:Key"]}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync(); 
                dynamic result =  JsonConvert.DeserializeObject(responseStream);
                name = result.results[0].formatted_address;
            }
            else
            {
                name = "N/A";
            }
            return name;
        }
    }
}
