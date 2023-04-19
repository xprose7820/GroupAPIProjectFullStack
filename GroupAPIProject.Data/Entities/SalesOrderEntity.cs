using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class SalesOrderEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Customer")]
        public int CusomterId { get; set; }
        public virtual CustomerEntity Customer { get; set; }
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        [Required]
        [ForeignKey("InventoryItem")]
        public int InventoryItemId { get; set; }
        public virtual InventoryItemEntity InventoryItem { get; set; }
        [Required]
        public DateTimeOffset OrderDate { get; set; }
        public virtual List<SalesOrderItemEntity> ListOfSalesOrderItems{get;set;} = new List<SalesOrderItemEntity>();

    }
}