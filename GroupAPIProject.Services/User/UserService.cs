using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.User;

namespace GroupAPIProject.Services.User
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(UserRegister model)
        {
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
    }
}