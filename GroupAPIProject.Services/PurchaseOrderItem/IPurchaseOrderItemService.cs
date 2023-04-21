using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.PurchaseOrderItem;

namespace GroupAPIProject.Services.PurchaseOrderItem
{
    public interface IPurchaseOrderItemService
    {
        Task<bool> CreatePurchaseOrderItemAsync(PurchaseOrderItemCreate model);
        // Task<bool> UpdatePurchaseOrderItemAsync(PurchaseOrderItemUpdate model);
    }
}