using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.SalesOrder
{
    public class SalesOrderCreate
    {
       
        public int CusomterId { get; set; }
      
        public int RetailerId { get; set; }
      
        public DateTimeOffset OrderDate { get; set; }
        
    }
}