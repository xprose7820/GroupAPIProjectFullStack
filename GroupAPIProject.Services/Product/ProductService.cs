using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Product;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Services.Product
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CreateProductAsync(ProductCreate model)
        {
            SupplierEntity supplierExists = await _dbContext.Suppliers.FindAsync(model.SupplierId);
            if (supplierExists == null) 
            {
                return false;
            }
            ProductEntity entity = new ProductEntity
            {
                SupplierId = model.SupplierId,
                ProductName = model.ProductName,
                Description = model.Description,
                Category = model.Category,
                Price = model.Price,
            };
            _dbContext.Products.Add(entity);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;

        }

    }
}
