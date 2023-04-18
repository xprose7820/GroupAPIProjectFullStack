using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Location;

namespace GroupAPIProject.Services.Location
{
    public interface ILocationService
    {
        Task<bool> CreateLocationAsync(LocationCreate request);
        Task<bool> RemoveLocationAsync(string LocationName);
    }
}