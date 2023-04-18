using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.PurchaseOrder;

namespace GroupAPIProject.Services.PurchaseOrder
{
    public interface IPurchaseOrderService
    {
        Task<bool> CreatePurchaseOrderAsync(PurchaseOrderCreate model);
        Task<bool> UpdatePurchaseOrderAsync(PurchaseOrderUpdate model);
    }
}