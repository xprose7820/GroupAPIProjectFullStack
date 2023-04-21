using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.SalesOrderItem
{
    public class SalesOrderItemCreate
    {
     
        public int SalesOrderId { get; set; }
        public int InventoryItemId{get;set;}
        
        public int Quantity { get; set; }
        
        public double Price { get; set; }
    }
}