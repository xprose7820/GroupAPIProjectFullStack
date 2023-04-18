using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.InventoryItem
{
    public class InventoryItemDetail
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public int Stock { get; set; }
    }
}
