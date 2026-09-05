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

        [HttpPost]
        public async Task<ActionResult<UserResponseDto>> Register(RegisterUserDto dto)
        {
            var userResponse = await _userService.RegisterAsync(dto);

            return Created(string.Empty, userResponse);
        }
    } 
}
