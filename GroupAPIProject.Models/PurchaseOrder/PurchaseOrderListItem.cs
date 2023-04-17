using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.PurchaseOrder
{
    public class PurchaseOrderListItem
    {
        public int Id{get;set;}
        public int SupplierId{get;set;}

        public DateTimeOffset OrderDate{get;set;}
    }
}