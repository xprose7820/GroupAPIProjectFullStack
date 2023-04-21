using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Location;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly ApplicationDbContext _context;

        public LocationService(ApplicationDbContext context) => _context = context;

        public async Task<bool> CreateLocationAsync (LocationCreate request)
        {
            LocationEntity locationEntity = new LocationEntity
            {
                LocationName = request.LocationName
                RetailerId = _retailerId,
            };
            _context.Locations.Add(locationEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> RemoveLocationAsync(string LocationName)
        {
            var locationEntity = await _context.Locations.FirstOrDefaultAsync(s => s.LocationName == LocationName);

            if (locationEntity == null)
            {
                return false;
            }
            _context.Locations.Remove(locationEntity);
            return await _context.SaveChangesAsync() == 1;
        }
    }
}