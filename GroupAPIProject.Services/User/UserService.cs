using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.User;
using Microsoft.AspNetCore.Identity;

namespace GroupAPIProject.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUserAsync(UserCreate newUser)
        {
            if (newUser.Role == "Admin")
            {
                AdminEntity entity = new AdminEntity
                {
                    Username = newUser.UserName
                };
                PasswordHasher<AdminEntity> passwordHasher = new PasswordHasher<AdminEntity>();
                entity.Password = passwordHasher.HashPassword(entity, newUser.Password);
                _context.Users.Add(entity);
                int numberOfChanges = await _context.SaveChangesAsync();
                return numberOfChanges == 1;
            }

            if (newUser.Role == "Retailer")
            {
                RetailerEntity entity = new RetailerEntity
                {
                    Username = newUser.UserName
                };
                PasswordHasher<RetailerEntity> passwordHasher = new PasswordHasher<RetailerEntity>();
                entity.Password = passwordHasher.HashPassword(entity, newUser.Password);
                _context.Users.Add(entity);
                int numberOfChanges = await _context.SaveChangesAsync();
                return numberOfChanges == 1;
            }
            int counter = await _context.SaveChangesAsync();
            return counter == 1;
            if(model.Role == "Admin"){
                AdminEntity entity = 
            }
            // if (await GetUserByUsername(model.Username) != null)
            // {
            //     return false;
            // }
            // UserEntity entity = new UserEntity
            // {
            //     Username = model.Username
            // };
             PasswordHasher<UserEntity> passwordHasher = new PasswordHasher<UserEntity>();
            // entity.Password = passwordHasher.HashPassword(entity, model.Password);
            // _context.Users.Add(entity);
            // int numberOfChanges = await _context.SaveChangesAsync();
            // return numberOfChanges == 1;
        }
        public async Task<UserEntity> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Username.ToLower() == username.ToLower());
        }




        // public async Task<bool> RegisterAdminAsync(AdminCreate request)
        // {
        //     var adminEntity = new AdminEntity
        //     {
        //         Username = request.Username,
        //         Password = request.Password
        //     };

        //     _context.Users.Add(adminEntity);

        //     var numberOfChanges = await _context.SaveChangesAsync();
        //     return numberOfChanges == 1;

        // }

        // public async Task<bool> RegisterRetailerAsync(RetailerCreate request)
        // {
        //     var retailerEntity = new RetailerEntity
        //     {
        //         Username = request.Username,
        //         Password = request.Password,
        //         Locations = request.Locations,
        //         InventoryItems = request.InventoryItems,
        //         PurchaseOrders = request.PurchaseOrders,
        //         SalesOrders = request.SalesOrders,
        //         PurchaseOrderItems = request.PurchaseOrderItems,
        //         SalesOrderItems = request.SalesOrderItems
        //     };

        //     _context.Users.Add(retailerEntity);

        //     var numberOfChanges = await _context.SaveChangesAsync();
        //     return numberOfChanges == 1;
        // }


    }
}