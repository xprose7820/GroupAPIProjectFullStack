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
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}