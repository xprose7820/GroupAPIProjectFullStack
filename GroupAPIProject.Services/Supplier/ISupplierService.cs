using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Supplier;

namespace GroupAPIProject.Services.Supplier
{
    public interface ISupplierService
    {
        Task<bool> CreateSupplierAsync(SupplierCreate request);
        Task<bool> RemoveSupplierAsync(int SupplierId);
        Task<bool> GetSupplierByIdAsync(int userId);

    }
}