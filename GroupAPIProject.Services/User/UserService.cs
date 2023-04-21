using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            if (newUser.Role.ToLower() == "admin")
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

            if (newUser.Role.ToLower() == "retailer")
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

        public async Task<bool> RemoveAdminAsync(int userId)
        {
            var userEntity = await _context.Users.OfType<AdminEntity>().FirstOrDefaultAsync(g => g.Id == userId);
            if (userEntity == null)

                return false;

            _context.Users.Remove(userEntity);
            return await _context.SaveChangesAsync() == 1;
        }


        public async Task<bool> RemoveRetailerAsync(int userId)
        {
            var userEntity = await _context.Users.OfType<RetailerEntity>().FirstOrDefaultAsync(g => g.Id == userId);
            if (userEntity.Locations.Count == 0)
            {
                return false;
            }
            if (userEntity.PurchaseOrders.Count == 0)
            {
                return false;
            }
            if (userEntity.SalesOrders.Count == 0)
            {
                return false;
            }
            _context.Users.Remove(userEntity);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<UserList>> GetUserListAsync()
        {

            IEnumerable<UserList> users = await _context.Users.Select(entity => new UserList
            {
                Role = entity.GetType().Name,
                Id = entity.Id,
                UserName = entity.Username
            }).ToListAsync();
            return users;
        }

        public async Task<bool> UpdateUserAsync(UserCreate update)
        {
            if (update.Role.ToLower() == "admin")
            {
                var userEntity = await _context.Users.FindAsync(update);
                if (userEntity.Id != null)
                    return false;

                userEntity.Username = update.UserName;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
            }
            if (update.Role.ToLower() == "retailer")
            {
                var userEntity = await _context.Users.FindAsync(update);
                if (userEntity.Id != null)
                    return false;

                userEntity.Username = update.UserName;

            var numberOfChanges = await _context.SaveChangesAsync();
            return numberOfChanges == 1;
            }
            int counter = await _context.SaveChangesAsync();
            return counter == 1;
        }
    }
}

        