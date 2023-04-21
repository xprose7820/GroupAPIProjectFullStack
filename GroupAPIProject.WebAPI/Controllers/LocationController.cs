using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Location;
using GroupAPIProject.Services.Location;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace GroupAPIProject.WebAPI.Controllers
{ [Authorize(Policy = "CustomRetailerEntity")]
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(LocationCreate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _locationService.CreateLocationAsync(model))
            {
                return Ok("Location added to database");
            }
            return BadRequest("Location could not be added to database");
        }

        [HttpDelete("{locationId:int}")]
        public async Task<IActionResult> RemoveLocation([FromRoute] int locationId)
        {
            return await _locationService.RemoveLocationAsync(locationId)
                ? Ok($"Location {locationId} was deleted successfully.")
                : BadRequest($"Location {locationId} could not be deleted.");
        }
        [HttpGet]
        public async Task<IActionResult> GetSupplierListAsync()
        {
            var LocationsToDisplay = await _locationService.GetLocationListAsync();
            return Ok(LocationsToDisplay);
        }
    }
}