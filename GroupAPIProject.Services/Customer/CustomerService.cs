 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Customer;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> RemoveCustomerAsync(string customerName)
        {
            var customerEntity = await _context.Customers.FirstOrDefaultAsync(n => n.CustomerName == customerName);

            if (customerEntity == null)

                return false;

            _context.Customers.Remove(customerEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateCustomerAsync(CustomerRegister update)
        {
            var customerEntity = await _context.Customers.FindAsync(update);
            if (customerEntity.Id != null)
                return false;
            
            customerEntity.CustomerName = update.CustomerName;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
            
        }

        public async Task<IEnumerable<CustomerList>> GetCustomerListsAsync()
        {
            var customer = await _context.Customers
                .Select(entity => new CustomerList
                {
                    Id = entity.Id,
                    CustomerName = entity.CustomerName
                }).ToListAsync();
            
            return customer;
        }

        
    }
}