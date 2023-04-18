using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.PurchaseOrderItem;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.PurchaseOrderItem
{
    public class PurchaseOrderItemService : IPurchaseOrderItemService
    {
        private readonly int _retailerId;
        private readonly ApplicationDbContext _dbContext;
        public PurchaseOrderItemService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _retailerId);
            if (!validId)
                throw new Exception("Attempted to build  without Retailer Id claim.");


            _dbContext = dbContext;
        }

        public async Task<bool> CreatePurchaseOrderItemAsync(PurchaseOrderItemCreate model)
        {
            RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            if (retailerExists is null)
            {
                return false;
            }
            // SupplierEntity supplierExists = await _dbContext.Suppliers.FirstOrDefaultAsync(g => g.Id == model.SupplierId);
            ProductEntity productExists = await _dbContext.Products.FindAsync(model.ProductId);

            if (productExists is null)
            {
                return false;
            }
            PurchaseOrderEntity purchaseOrderExists = await _dbContext.PurchaseOrders.FindAsync(model.PurchaseOrderId);

            if (purchaseOrderExists is null || purchaseOrderExists.RetailerId != _retailerId)
            {
                return false;
            }

            PurchaseOrderItemEntity entity = new PurchaseOrderItemEntity
            {
                RetailerId = _retailerId,
                ProductId = model.ProductId,
                PurchaseOrderId = model.PurchaseOrderId,
                Quantity = model.Quantity,
                Price = productExists.Price
            };
            // aftr creating a PurchaseOrderItemEntity, need to later add to an existing InventoryItem


            _dbContext.PurchaseOrderItems.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }



    }
}