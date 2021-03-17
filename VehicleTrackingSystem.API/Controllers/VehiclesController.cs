using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using VehicleTrackingSystem.API.DTO;
using VehicleTrackingSystem.API.Enumerations;
using VehicleTrackingSystem.API.Services;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.API.Controllers
{
    [Route("api/[controller]/v1")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleTrackingService _vehicleTrackingService;

        public VehiclesController(IVehicleTrackingService vehicleTrackingService)
        {
            _vehicleTrackingService = vehicleTrackingService;
        }

        [Authorize(Roles = "User, Admin")]
        [HttpPost]
        [Route("register-vehicle")]
        public async Task<IActionResult> RegisterVehicle([FromBody] VehicleRegisterDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicleIsNew = await _vehicleTrackingService.IsVehicleNew(model.Brand, model.ChasisNumber);
            if (vehicleIsNew)
            {
                var trackingId = await _vehicleTrackingService.RegisterVehicle(model, User.FindFirstValue(ClaimTypes.NameIdentifier));
                return CreatedAtRoute("vehicle", new { trackid = trackingId }, null);
            }

            return BadRequest(new ApiResponse
            {
                Code = ResponseEnum.DuplicateVehicle.ResponseCode(),
                Description = ResponseEnum.DuplicateVehicle.DisplayName()
            });
        }

        [HttpGet("{trackid}", Name = "vehicle")]
        public async Task<IActionResult> Vehicle(string trackid)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await _vehicleTrackingService.GetVehicleDtoByTrackingId(trackid.Trim());

            return Ok(new ApiResponseData 
            {
                Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(),
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName(), 
                Data = vehicle
            });
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [Route("record-vehicle-position")]
        public async Task<IActionResult> AddVehiclePosition([FromBody] VehicleLocationDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Vehicle vehicle = await GetVehicle(model.TrackingId);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != vehicle.UserId) return StatusCode(403);

            if (vehicle == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = ResponseEnum.InvalidVehicleId.ResponseCode(),
                    Description = ResponseEnum.InvalidVehicleId.DisplayName(),
                });
            }
            await _vehicleTrackingService.AddVehiclePosition(model, vehicle.Id);
            return Ok(new ApiResponse
            {
                Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(),
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName()
            });
        }       

        [Authorize(Roles = "Admin")]
        [HttpGet("get-current-vehicle-position/{trackingId}")]
        public async Task<IActionResult> GetCurrentVehiclePosition(string trackingId)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await GetVehicle(trackingId);

            if (vehicle == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = ResponseEnum.InvalidVehicleId.ResponseCode(),
                    Description = ResponseEnum.InvalidVehicleId.DisplayName(),
                });
            }

            var location = await _vehicleTrackingService.GetVehicleLocation(vehicle.Id);
            return Ok(new ApiResponseData
            {
                Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(),
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName(),
                Data = location
            });
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("get-vehicle-positions")]
        public async Task<IActionResult> GetVehiclePositions([FromBody]VehiclePositionsDto model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vehicle = await GetVehicle(model.TrackingId);

            if (vehicle == null)
            {
                return BadRequest(new ApiResponse
                {
                    Code = ResponseEnum.InvalidVehicleId.ResponseCode(),
                    Description = ResponseEnum.InvalidVehicleId.DisplayName(),
                });
            }

            var locations = await _vehicleTrackingService.GetVehicleLocations(vehicle.Id, model.From, model.To);
            return Ok(new ApiResponseData
            {
                Code = ResponseEnum.ApprovedOrCompletedSuccesfully.ResponseCode(),
                Description = ResponseEnum.ApprovedOrCompletedSuccesfully.DisplayName(),
                Data = locations
            });
        }

        private async Task<Vehicle> GetVehicle(string trackingId)
        {
            return await _vehicleTrackingService.GetVehicleByTrackingId(trackingId.Trim());
        }

    }
}
