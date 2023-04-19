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
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId{ get ;set; }
        public virtual PurchaseOrderEntity PurchaseOrder{ get; set; }
        // should pull productName from purchaseOrder, then purchaseOrderItem
        [Required]
        public int ProductId{ get ;set; }

        [Required]
        [ForeignKey("Location")]
        public int LocationId{ get; set; }
        public virtual LocationEntity Location{ get; set; }
        
        [Required]
        public int Stock{ get; set; }
        public virtual List<SalesOrderEntity> ListOfSalesOrders{get;set;} = new List<SalesOrderEntity>();

    }
}