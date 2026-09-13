using EventHub.API.Entities.Enums;

namespace EventHub.API.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public Role UserRole { get; set; }
    }
}
