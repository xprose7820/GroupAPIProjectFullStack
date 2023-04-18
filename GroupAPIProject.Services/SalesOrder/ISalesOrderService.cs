using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.SalesOrder;

namespace GroupAPIProject.Services.SalesOrder
{
    public interface ISalesOrderService
    {
        public Task<bool> CreateSalesOrderAsync(SalesOrderCreate model);
    }
}