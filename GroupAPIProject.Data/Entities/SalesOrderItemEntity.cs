using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class SalesOrderItemEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("SalesOrder")]
        public int SalesOrderId { get; set; }
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