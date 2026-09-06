using EventHub.API.Entities;

namespace EventHub.API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
