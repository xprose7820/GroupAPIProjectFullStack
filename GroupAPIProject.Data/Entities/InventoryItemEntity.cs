using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class InventoryItemEntity
    {
        [Key]
        public int Id{ get; set; }
        
        [Required]
        [ForeignKey("PurchaseOrderItem")]
        public int PurchaseOrderItemId{ get ;set; }
        public virtual PurchaseOrderItemEntity PurchaseOrderItem{ get; set; }
        // should pull productName from purchaseOrderItem, not directly from anywhere else
        [Required]
        public string ProductName{ get ;set; }

        [Required]
        [ForeignKey("Location")]
        public int LocationId{ get; set; }
        public virtual LocationEntity Location{ get; set; }
        
        [Required]
        public int Stock{ get; set; }
        

    }
}