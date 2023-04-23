using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.User;

namespace GroupAPIProject.Services.User
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(UserCreate newUser);
        Task<bool> UpdateUserAsync(UserCreate update);
        Task<bool> RemoveAdminAsync(UserCreate remove);
        Task<bool> RemoveRetailerAsync(UserCreate remove);
        Task<IEnumerable<UserList>> GetUserListAsync();

    }
}