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
        }


    }
}