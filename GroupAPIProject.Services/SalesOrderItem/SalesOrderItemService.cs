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
    public class SalesOrderItemService : ISalesOrderItemService
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
            SalesOrderEntity salesOrderExists = await _dbContext.SalesOrders.Where(entity => entity.RetailerId == _retailerId).FirstOrDefaultAsync(g => g.Id == model.SalesOrderId);
            if(salesOrderExists is null){
                return false;
            }
            InventoryItemEntity inventoryItemExists = await _dbContext.Locations.Where(entity => entity.Id == salesOrderExists.LocationId)
                .Include(g => g.ListOfInventoryItems).SelectMany(g => g.ListOfInventoryItems).FirstOrDefaultAsync(g => g.Id == model.InventoryItemId);

            if(inventoryItemExists is null){
                return false;
            }

            SalesOrderItemEntity entity = new SalesOrderItemEntity{
                ProductName = inventoryItemExists.ProductName,
                SalesOrderId = model.SalesOrderId,
                InventoryItemId = model.InventoryItemId,
                Quantity = model.Quantity,
                Price = model.Price
            };


            inventoryItemExists.Stock = inventoryItemExists.Stock - model.Quantity;
            LocationEntity locationExists = await _dbContext.Locations.Where(entity => entity.Id == salesOrderExists.LocationId).FirstOrDefaultAsync(g => g.Id == salesOrderExists.LocationId);
            if(locationExists is null){
                return false;
            }
            locationExists.Capacity = locationExists.Capacity + model.Quantity;
            

            _dbContext.SalesOrderItems.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 3;

            // RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            // if (retailerExists is null)
            // {
            //     return false;
            // }
            // // SupplierEntity supplierExists = await _dbContext.Suppliers.FirstOrDefaultAsync(g => g.Id == model.SupplierId);
            // InventoryItemEntity inventoryItemExists = await _dbContext.InventoryItems.FindAsync(model.InventoryItemId);

            // if (inventoryItemExists is null || inventoryItemExists.RetailerId != _retailerId)
            // {
            //     return false;
            // }
            // SalesOrderEntity salesOrderExists = await _dbContext.SalesOrders.FindAsync(model.SalesOrderId);

            // if (salesOrderExists is null || salesOrderExists.RetailerId != _retailerId)
            // {
            //     return false;
            // }

            // SalesOrderItemEntity entity = new SalesOrderItemEntity
            // {
            //     RetailerId = _retailerId,
            //     InventoryItemId = model.InventoryItemId,
            //     SalesOrderId = model.SalesOrderId,
            //     Quantity = model.Quantity,
            //     Price = model.Price
            // };

            // LocationEntity locationExists = await _dbContext.Locations.FindAsync(inventoryItemExists.LocationId);
            // if(locationExists is null || locationExists.RetailerId != _retailerId){
            //     return false;
            // }
           
            // locationExists.Capacity = locationExists.Capacity + model.Quantity;
            // inventoryItemExists.Stock = inventoryItemExists.Stock - model.Quantity;


            // _dbContext.SalesOrderItems.Add(entity);
            // int numberOfChanges = await _dbContext.SaveChangesAsync();
            // return numberOfChanges == 3;

        }

        // public async Task<bool> UpdateSalesOrderItemAsync(SalesOrderItemUpdate model){
        //     SalesOrderItemEntity salesOrderItemExists = await _dbContext.SalesOrderItems.FindAsync(model.Id);
        //     if(salesOrderItemExists is null || salesOrderItemExists.RetailerId != _retailerId){
        //         return false;
        //     }
        //     int originalQuantity = salesOrderItemExists.Quantity;
        //     salesOrderItemExists.Quantity = model.Quantity;
        //     // need to update the inventory item/ location
        //     InventoryItemEntity inventoryItemExists = await _dbContext.InventoryItems.FirstAsync()
            
        // }




        


    }
}