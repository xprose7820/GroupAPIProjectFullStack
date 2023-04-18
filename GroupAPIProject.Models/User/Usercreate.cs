using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupAPIProject.Models.User
{
    public class UserCreate
    {
        public string Role{ get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}