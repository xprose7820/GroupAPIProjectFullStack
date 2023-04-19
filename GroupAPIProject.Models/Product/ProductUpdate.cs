using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.Product
{
    public class ProductUpdate
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string?Category { get; set; }
        public double Price { get; set; }
    }
}
