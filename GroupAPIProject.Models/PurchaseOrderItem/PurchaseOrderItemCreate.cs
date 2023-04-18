using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.PurchaseOrderItem
{
    public class PurchaseOrderItemCreate
    {

        public int PurchaseOrderId { get; set; }

        public int RetailerId { get; set; }


        public int ProductId { get; set; }

        public int Quantity { get; set; }


    }
}