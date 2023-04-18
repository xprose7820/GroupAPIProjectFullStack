using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.InventoryItem
{
    public class InventoryItemCreate
    {
        public int PurchaseOrderId{get;set;}
        public int ProductId { get; set; }
        public int LocationId { get; set; }
        public int RetailerId { get; set; }
        public int Stock { get; set; }
    }
}
