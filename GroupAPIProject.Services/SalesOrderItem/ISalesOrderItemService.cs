using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Services.SalesOrderItem
{
    public interface ISalesOrderItemService
    {
                Task<bool> CreateSalesOrderItemAsync(PurchaseOrderItemCreate model);

    }
}