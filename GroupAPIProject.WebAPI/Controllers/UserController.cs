using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Services.Token;
using GroupAPIProject.Services.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [ApiController]
    public class UserController
    {
        private readonly IUserService _userService; 
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService){
            _userService = userService;
            _tokenService = tokenService;
        }   

        
    }
}