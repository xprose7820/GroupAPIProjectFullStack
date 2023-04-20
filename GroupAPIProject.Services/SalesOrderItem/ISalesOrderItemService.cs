using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.SalesOrderItem;

namespace GroupAPIProject.Services.SalesOrderItem
{
    public interface ISalesOrderItemService
    {
        Task<bool> CreateSalesOrderItemAsync(SalesOrderItemCreate model);
        // Task<bool> UpdatSalesOrderItemAsync(SalesOrderItemUpdate model);
    }
}