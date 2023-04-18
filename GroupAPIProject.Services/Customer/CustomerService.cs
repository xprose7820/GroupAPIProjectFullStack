using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Customer;

namespace GroupAPIProject.Services.Customer
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCustomerAsync(CustomerRegister newCustomer)
        {
            var customerEntity = new CustomerEntity
            {
                CustomerName = newCustomer.CustomerName
            };

            _context.Customers.Add(customerEntity);

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
        }
    }
}