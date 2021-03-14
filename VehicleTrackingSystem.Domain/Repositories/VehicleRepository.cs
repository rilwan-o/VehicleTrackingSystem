﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleTrackingSystem.Domain.Models;

namespace VehicleTrackingSystem.Domain.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        public readonly ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public VehicleRepository(ApplicationDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<bool> VehicleExists(string chasis)
        {
            var existingVehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.ChasisNumber == chasis && v.Status == _configuration["Vehicle:Status"]);
            return existingVehicle == null;
        }
        public async Task<Vehicle> GetVehicleByTrackingId(string trackingId)
        {
            var vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(v => v.TrackingId == trackingId);
            return vehicle;
        }
        public async Task AddVehicle(Vehicle vehicle)
        {
            await _dbContext.Vehicles.AddAsync(vehicle);
            await _dbContext.SaveChangesAsync();
        }
    }
}
