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
    }
}