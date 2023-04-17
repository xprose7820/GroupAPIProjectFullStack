using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class PurchaseOrderEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        public virtual SupplierEntity Supplier { get; set; }
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        [Required]
        public DateTimeOffset OrderDate { get; set; }
        public virtual List<PurchaseOrderItemEntity> ListOfPurchaseOrderItems{get;set;} = new List<PurchaseOrderItemEntity>();

    }
}