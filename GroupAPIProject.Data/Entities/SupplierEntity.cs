using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class SupplierEntity
    {
        [Key]
        public int Id{get;set;}
        [Required]
        public string SupplierName{get;set;}
        public virtual List<ProductEntity>? ListOfProducts{get;set;} = new List<ProductEntity>();
        public virtual List<PurchaseOrderEntity>? ListOfPurchaseOrders{get;set;} = new List<PurchaseOrderEntity>();
    }
}