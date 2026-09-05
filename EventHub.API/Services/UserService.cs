using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EventHub.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<UserResponseDto> RegisterAsync(RegisterUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user != null)
            {
                throw new InvalidOperationException("Email already registered.");
            }

            User newUser = new User
            (
                dto.Name,
                dto.Email,
                Entities.Enums.Role.Customer
            );

            string passwordHash = _passwordHasher.HashPassword(newUser, dto.Password);

            newUser.PasswordHash = passwordHash;

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveChangesAsync();

            UserResponseDto userResponse = new UserResponseDto
            {
                Id = newUser.Id,
                Name = newUser.Name,
                Email = newUser.Email,
                UserRole = newUser.UserRole
            };

            return userResponse;
        }
    }
}
