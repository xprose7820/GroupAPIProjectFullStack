using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.PurchaseOrder
{
    public class PurchaseOrderDetail
    {
        public int Id{get;set;}
        public int SupplierId{get;set;}

        public DateTimeOffset OrderDate{get;set;}
        public DateTimeOffset ModifiedDate{get;set;}
    }
}