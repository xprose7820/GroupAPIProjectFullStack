using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Supplier;
using Microsoft.EntityFrameworkCore;

namespace GroupAPIProject.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;

        public SupplierService(ApplicationDbContext context) => _context = context;

        public async Task<bool> CreateSupplierAsync (SupplierCreate request)
        {
            SupplierEntity supplierEntity = new SupplierEntity
            {
                SupplierName = request.SupplierName
            };
            _context.Suppliers.Add(supplierEntity);
            int numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }

        public async Task<bool> RemoveSupplierAsync(int SupplierId)
        {
            var supplierEntity = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == SupplierId);

            if (supplierEntity == null)
            {
                return false;
            }
            _context.Suppliers.Remove(supplierEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<SupplierDetail> GetSupplierByIdAsync(int SupplierId)
        {
            SupplierEntity entity = await _context.Suppliers.FindAsync(SupplierId);
            if (entity is null)
                return null;

            var SupplierDetail = new SupplierDetail
            {
                Id = entity.Id,
                SupplierName = entity.SupplierName
        };
            return SupplierDetail;
        }

        public async Task<IEnumerable<SupplierDetail>> GetSupplierListAsync()
        {
            var SupplierToDisplay = await _context.Suppliers
                .Select(entity => new SupplierDetail
                {
                    Id = entity.Id,
                    SupplierName = entity.SupplierName
                }).ToListAsync();
            
            return SupplierToDisplay;
        }
    }
}