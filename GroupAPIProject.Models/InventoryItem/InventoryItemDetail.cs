﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.InventoryItem
{
    public class InventoryItemDetail
    {
    
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
        public int LocationId { get; set; }
        public int RetailerId { get; set; }
        public int Stock { get; set; }
    }
}
