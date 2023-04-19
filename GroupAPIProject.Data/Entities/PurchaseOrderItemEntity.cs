using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GroupAPIProject.Data.Entities
{
    public class PurchaseOrderItemEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrderEntity PurchaseOrder { get; set; }
        // should pull product name from Purchase then supplier then product 
        [Required]
        public int ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
       
    }
}