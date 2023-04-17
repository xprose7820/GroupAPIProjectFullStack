using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace GroupAPIProject.Data.Entities
{
    public class LocationEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string LocationName { get; set; }
        [Required]
        public int Capacity { get; set; }
        [Required]
        [ForeignKey("Retailer")]
        public int RetailerId { get; set; }
        public virtual RetailerEntity Retailer { get; set; }
        public virtual List<InventoryItemEntity> InventoryItems { get; set; } = new List<InventoryItemEntity>();
    }
}