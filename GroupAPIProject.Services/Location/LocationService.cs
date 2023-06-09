using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Location;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.Location
{
    public class LocationService : ILocationService
    {
       private readonly int _retailerId;
        private readonly ApplicationDbContext _context;

        public LocationService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            ClaimsIdentity? userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            string value = userClaims.FindFirst("Id")?.Value;
            bool validId = int.TryParse(value, out _retailerId);
            if (!validId)
            {
                throw new Exception("Attempted to build without Retailer Id Claim");
            }
            _context = dbContext;
        }

        public async Task<bool> CreateLocationAsync (LocationCreate request)
        {
            LocationEntity locationEntity = new LocationEntity
            {

                LocationName = request.LocationName,
                RetailerId = _retailerId,

            };
            _context.Locations.Add(locationEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> RemoveLocationAsync(int LocationId)
        {
            var locationEntity = await _context.Locations.Where(entity => entity.RetailerId == _retailerId).FirstOrDefaultAsync(s => s.Id == LocationId);

            if (locationEntity == null)
            {
                return false;
            }
            if (locationEntity.ListOfInventoryItems.Count == 0)
            {
                return false;
            }
            _context.Locations.Remove(locationEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<LocationDetail>> GetLocationListAsync()
        {
            var LocationToDisplay = await _context.Locations.Where(entity => entity.RetailerId == _retailerId)
                .Select(entity => new LocationDetail
                {
                    Id = entity.Id,
                    LocationName = entity.LocationName,
                    Capacity = entity.Capacity
                }).ToListAsync();
            
            return LocationToDisplay;
        }
    }
}