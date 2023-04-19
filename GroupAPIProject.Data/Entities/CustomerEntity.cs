using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Data.Entities
{
    public class CustomerEntity
    {

        [Key]
        public int Id{get;set;}
        
        [Required]
        public string CustomerName{get;set;}

        public virtual List<SalesOrderEntity> SalesOrders{get;set;} = new List<SalesOrderEntity>();

    }

}