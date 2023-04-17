using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.PurchaseOrder
{
    public class PurchaseOrderCreate
    {

        public int SupplierId { get; set; }

        public int RetailerId { get; set; }

        public DateTimeOffset OrderDate { get; set; }
    }
}