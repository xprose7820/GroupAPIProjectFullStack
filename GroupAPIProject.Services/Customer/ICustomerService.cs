using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Customer;

namespace GroupAPIProject.Services.Customer
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsyn(CustomerRegister newCustomer);

        Task<bool> RemoveCustomerAsyn(CustomerRegister remove);

    }
}