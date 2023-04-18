using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Models.User;
using GroupAPIProject.Services.Token;
using GroupAPIProject.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace GroupAPIProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        
        [HttpPost("Register")]
        public async Task<IActionResult> CreateUser([FromBody]UserCreate newUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool createUser = await _userService.CreateUserAsync(newUser);
            if (createUser)
            {
                return Ok("User was created.");
            }
            return BadRequest("User could not be created.");

        }

          
    }
}