using EventHub.API.Entities.Enums;

namespace EventHub.API.DTOs
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Role UserRole { get; set; }
    }
}
