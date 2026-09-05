using EventHub.API.DTOs;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterUserDto dto)
        {
            var userResponse = await _userService.RegisterAsync(dto);

            return Created(string.Empty, userResponse);
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> Login(LoginUserDto dto)
        {
            var loginResponse = await _userService.VerifyLogin(dto);

            return Ok(loginResponse);
        }
    } 
}
