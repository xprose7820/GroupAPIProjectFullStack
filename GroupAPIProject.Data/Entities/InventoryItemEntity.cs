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
        public int Id{get;set;}
        [Required]
        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId{get;set;}
        public virtual PurchaseOrderEntity PurchaseOrderEntity{get;set;}
        [Required]
        [ForeignKey("Location")]
        public int LocationId{get;set;}
        public virtual LocationEntity Location{get;set;}
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        [Required]
        public int Stock{get;set;}

    }
}