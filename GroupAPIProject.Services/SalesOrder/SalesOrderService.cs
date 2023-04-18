using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.SalesOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.SalesOrder
{
    public class SalesOrderService : ISalesOrderService
    {
        private readonly int _retailerId;
        private readonly ApplicationDbContext _dbContext;

        public SalesOrderService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext)
        {
            var userClaims = httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var value = userClaims.FindFirst("Id")?.Value;
            var validId = int.TryParse(value, out _retailerId);
            if (!validId)
                throw new Exception("Attempted to build NoteService without Retailer Id claim.");


            _dbContext = dbContext;
        }
        public async Task<bool> CreateSalesOrderAsync(SalesOrderCreate model){
            RetailerEntity retailerExists = await _dbContext.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == model.RetailerId);
            if (retailerExists is null)
            {
                return false;
            }
            // SupplierEntity supplierExists = await _dbContext.Suppliers.FirstOrDefaultAsync(g => g.Id == model.SupplierId);
            CustomerEntity customerExists = await _dbContext.Customers.FindAsync(model.CusomterId);

            if (customerExists is null)
            {
                return false;
            }
            SalesOrderEntity entity = new SalesOrderEntity{
                CusomterId = model.CusomterId,
                RetailerId = model.RetailerId,
                OrderDate = DateTime.Now
            };
            _dbContext.SalesOrders.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }

    }
}