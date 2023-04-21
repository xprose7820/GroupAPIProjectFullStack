using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupAPIProject.Data.Entities;
using GroupAPIProject.Models.Token;
using GroupAPIProject.Models.User;
using GroupAPIProject.Services.Token;
using GroupAPIProject.Services.User;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> CreateUser([FromBody] UserCreate newUser)
        {
            if (!ModelState.IsValid)
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
        
        [HttpPost("~/api/TokenAdmin")]
        public async Task<IActionResult> TokenAdmin([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TokenResponse tokenResponse = await _tokenService.GetTokenAsync<AdminEntity>(request);
            if (tokenResponse is null)
            {
                return BadRequest("invalid username or password");
            }
            return Ok(tokenResponse);
        }
        
        [HttpPost("~/api/TokenRetailer")]
        public async Task<IActionResult> TokenRetailer([FromBody] TokenRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TokenResponse tokenResponse = await _tokenService.GetTokenAsync<RetailerEntity>(request);
            if (tokenResponse is null)
            {
                return BadRequest("invalid username or password");
            }
            return Ok(tokenResponse);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {

            IEnumerable<UserList> users = await _userService.GetUserListAsync();
            return Ok(users);

        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserById([FromBody] UserCreate update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return await _userService.UpdateUserAsync(update)
                ? Ok("User was updated successfully.")
                : BadRequest("User was unable to be updated.");
        }
        
    }
}