using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Services.Product
{
    public interface IProductService
    {
        Task<bool> CreateProductAsync(ProductCreate model);
        Task<IEnumerable<ProductListItem>> GetProductListAsync(int supplierId);


        Task<bool> UpdateProductAsync(ProductUpdate model);
        Task<bool> DeleteProductByIdAsync(ProductDelete model);

    }
}
