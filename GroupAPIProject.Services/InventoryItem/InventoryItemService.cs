﻿using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.InventoryItem;
using GroupAPIProject.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;


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
            RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            if (retailerExists == null || retailerExists.Id == _retailerId)
            {
                return false;
            }

            PurchaseOrderEntity purchaseOrderExists = await _dbContext.PurchaseOrders.FindAsync(model.PurchaseOrderId);
            if (purchaseOrderExists == null || purchaseOrderExists.Retailer.Id != _retailerId) 
            {
                return false;
            }

            LocationEntity locationExists = await _dbContext.Locations.FindAsync(model.LocationId);
            if (locationExists == null || locationExists.RetailerId != _retailerId) 
            {
                return false;
            }

            InventoryItemEntity entity = new InventoryItemEntity
            {
                RetailerId = model.RetailerId,
                PurchaseOrderId = model.PurchaseOrderId,
                LocationId = model.LocationId,
                Stock = model.Stock
            };

            locationExists.Capacity = locationExists.Capacity - model.Stock;


            _dbContext.InventoryItems.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 2;
        }

        public async Task<bool> InventoryItemUpdate(InventoryItemUpdate model)
        {
            InventoryItemEntity inventoryItemExists = await _dbContext.InventoryItems.FindAsync(model.Id);
            if (inventoryItemExists == null || inventoryItemExists.RetailerId != _retailerId)
            {
                return false;
            }
            else
            {
                inventoryItemExists.Stock = model.Stock;
            }

            int originalStock = inventoryItemExists.Stock;
            inventoryItemExists.Stock = model.Stock;

            PurchaseOrderEntity purchaseOrderExists = await _dbContext.PurchaseOrders.FindAsync(model.PurchaseOrderId);
            if (purchaseOrderExists == null || purchaseOrderExists.Retailer.Id != _retailerId)
            {
                return false;
            }
            LocationEntity locationExists = await _dbContext.Locations.FindAsync(model.LocationId);
            if (locationExists == null || locationExists.RetailerId != _retailerId)
            {
                return false;
            }
            locationExists.Capacity = model.Stock;
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 2;
        }
    }
}
