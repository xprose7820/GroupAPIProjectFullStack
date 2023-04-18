using GroupAPIProject.Data;
using GroupAPIProject.Models.InventoryItem;
using GroupAPIProject.Models.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Services.InventoryItem
{
    public class InventoryItemService : IInventoryItemService
    {
        private readonly int _retailerId;
        private readonly ApplicationDbContext _dbContext;

        public InventoryItemService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _retailerId);
            if (!validId)
            {
                throw new Exception("Attempted to build without Retailer Id Claim");
            }
            _dbContext = dbContext;
        }
        public async Task<bool> CreateInventoryItemAsync(InventoryItemCreate model)
        {
            
        }
    }
}
