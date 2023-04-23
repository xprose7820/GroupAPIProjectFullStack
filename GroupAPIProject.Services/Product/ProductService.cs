using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<ProductListItem>> GetProductListAsync(int supplierId)
        {
            SupplierEntity supplier = await _dbContext.Suppliers.FindAsync(supplierId);
            return supplier.ListOfProducts as IEnumerable<ProductListItem>;
        }


        public async Task<bool> UpdateProductAsync(ProductUpdate model)
        {
            ProductEntity productExists = await _dbContext.Suppliers.Where(g => g.Id == model.SupplierId)
                .Include(g => g.ListOfProducts).SelectMany(g => g.ListOfProducts).FirstOrDefaultAsync(g => g.ProductName == model.ProductName);
            if (productExists == null)
            {
                return false;
            }
            else
            {
                productExists.ProductName = model.ProductName;
                productExists.Description = model.Description;
                productExists.Category = model.Category;
                productExists.Price = model.Price;
            }
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }
        public async Task<bool> DeleteProductByIdAsync(ProductDelete model){
            ProductEntity productExists = await _dbContext.Suppliers.Where(entity => entity.Id == model.SupplierId)
                .Include(g => g.ListOfProducts).SelectMany(g => g.ListOfProducts).FirstOrDefaultAsync(g => g.Id == model.ProductId);
            if(productExists is null){
                return false;
            }
            bool someoneHasBoughtAProduct = await _dbContext.Suppliers.Include(g => g.ListOfPurchaseOrders)
                .SelectMany(g => g.ListOfPurchaseOrders.SelectMany(s => s.ListOfPurchaseOrderItems)).AnyAsync(s => s.ProductId == model.ProductId);
            if(someoneHasBoughtAProduct){
                return false;
            }
            _dbContext.Products.Remove(productExists);
            int numberOfChanges = await _dbContext.SaveChangesAsync();
            return numberOfChanges == 1;
        }


    }
}
