using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.PurchaseOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.PurchaseOrder
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly int _retailerId;
        private readonly ApplicationDbContext _dbContext;

        public PurchaseOrderService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _retailerId);
            if (!validId)
                throw new Exception("Attempted to build without Retailer Id claim.");


            _dbContext = dbContext;
        }

        public async Task<bool> CreatePurchaseOrderAsync(PurchaseOrderCreate model)
        {
            RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            if(retailerExists is null){
                return false;
            }
            SupplierEntity supplierEntity = await _dbContext.Suppliers.FindAsync(model.SupplierId);
            if(supplierEntity is null){
                return false;
            }

            PurchaseOrderEntity entity = new PurchaseOrderEntity{
                SupplierId = model.SupplierId,
                RetailerId = model.RetailerId,
                OrderDate = DateTime.Now
            };

            _dbContext.PurchaseOrders.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

            // RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            // if (retailerExists is null)
            // {
            //     return false;
            // }
            // // SupplierEntity supplierExists = await _dbContext.Suppliers.FirstOrDefaultAsync(g => g.Id == model.SupplierId);
            // SupplierEntity supplierExists = await _dbContext.Suppliers.FindAsync(model.SupplierId);

            // if (supplierExists is null)
            // {
            //     return false;
            // }
            // PurchaseOrderEntity entity = new PurchaseOrderEntity
            // {
            //     SupplierId = model.SupplierId,
            //     RetailerId = _retailerId,
            //     OrderDate = DateTime.Now
            // };
            // _dbContext.PurchaseOrders.Add(entity);
            // int numberOfChanges = await _dbContext.SaveChangesAsync();
            // return numberOfChanges == 1;

        }
        public async Task<bool> UpdatePurchaseOrderAsync(PurchaseOrderUpdate model)
        {
            PurchaseOrderEntity purchaseOrderExists = await _dbContext.PurchaseOrders.FindAsync(model.Id);
            if (purchaseOrderExists is null || purchaseOrderExists.RetailerId != _retailerId)
            {
                return false;
            }
            purchaseOrderExists.SupplierId = model.SupplierId;
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }
        

    }
}