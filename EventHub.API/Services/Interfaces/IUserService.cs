using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto> RegisterAsync(RegisterUserDto dto);
    }
}
