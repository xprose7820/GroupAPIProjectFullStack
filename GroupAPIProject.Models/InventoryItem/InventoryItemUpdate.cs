using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.InventoryItem
{
    public class InventoryItemUpdate
    {
        public int Id { get; set; }
        public int Stock { get; set; }
        public int PurchaseOrderId { get; set; }
        public int LocationId { get; set; }
    }
}
