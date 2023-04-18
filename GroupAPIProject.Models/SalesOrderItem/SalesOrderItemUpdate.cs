using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.SalesOrderItem
{
    public class SalesOrderItemUpdate
    {
     
        public int Id { get; set; }
        [Required]
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
        public virtual SalesOrderEntity SalesOrder { get; set; }
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        [Required]
        [ForeignKey("InventoryItem")]
        public int InventoryItemId { get; set; }
        public virtual InventoryItemEntity InventoryItem { get; set; }
        [Required]
        public int Quantity { get; set; }
        
    }
}