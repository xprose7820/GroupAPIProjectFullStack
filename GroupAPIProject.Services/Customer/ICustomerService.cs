using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.Customer;

namespace GroupAPIProject.Services.Customer
{
    public interface ICustomerService
    {
        Task<bool> CreateCustomerAsync(CustomerRegister newCustomer);

        Task<bool> RemoveCustomerAsync(string name);

        Task<bool> UpdateCustomerAsync(CustomerRegister update);

        Task<IEnumerable<CustomerList>> GetCustomerListsAsync();

    }
}