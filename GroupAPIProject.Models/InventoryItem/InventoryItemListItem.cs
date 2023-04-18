using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.InventoryItem
{
    public class InventoryItemListItem
    {
        public int Id { get; set; }
        [ForeignKey("Location")]
        public int LocationId { get; set; }
        public int Stock { get; set; }
    }
}
