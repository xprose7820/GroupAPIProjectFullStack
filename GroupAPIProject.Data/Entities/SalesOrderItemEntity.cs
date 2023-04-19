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
        public virtual SalesOrderEntity SalesOrder { get; set; }
        // should pull Productname from SalesOrder which pulls from Inventoryitem
        [Required]
        public int ProductName { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
    }
}