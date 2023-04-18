using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.SalesOrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.SalesOrderItem
{
    public class SalesOrderItemService
    {
        private readonly int _retailerId;
        private readonly ApplicationDbContext _dbContext;
        public SalesOrderItemService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _retailerId);
            if (!validId)
                throw new Exception("Attempted to build  without Retailer Id claim.");


            _dbContext = dbContext;
        }

        public async Task<bool> CreateSalesOrderItemAsync(SalesOrderItemCreate model)
        {
            RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            if (retailerExists is null)
            {
                return false;
            }
            // SupplierEntity supplierExists = await _dbContext.Suppliers.FirstOrDefaultAsync(g => g.Id == model.SupplierId);
            InventoryItemEntity inventoryItemExists = await _dbContext.InventoryItems.FindAsync(model.InventoryItemId);

            if (inventoryItemExists is null || inventoryItemExists.RetailerId != _retailerId)
            {
                return false;
            }
            SalesOrderEntity salesOrderExists = await _dbContext.SalesOrders.FindAsync(model.SalesOrderId);

            if (salesOrderExists is null || salesOrderExists.RetailerId != _retailerId)
            {
                return false;
            }

            SalesOrderItemEntity entity = new SalesOrderItemEntity
            {
                RetailerId = _retailerId,
                InventoryItemId = model.InventoryItemId,
                SalesOrderId = model.SalesOrderId,
                Quantity = model.Quantity,
                Price = model.Price
            };

            LocationEntity locationExists = await _dbContext.Locations.FindAsync(inventoryItemExists.LocationId);
            if(locationExists is null || locationExists.RetailerId != _retailerId){
                return false;
            }
           
            locationExists.Capacity = locationExists.Capacity - model.Quantity;
            inventoryItemExists.Stock = inventoryItemExists.Stock - model.Quantity;


            _dbContext.SalesOrderItems.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 3;

        }



        


    }
}