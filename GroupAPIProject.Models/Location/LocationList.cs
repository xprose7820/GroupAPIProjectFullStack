using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.Location
{
    public class LocationDetail
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public int Capacity { get; set; }
        public int RetailerId { get; set; }
    }
}