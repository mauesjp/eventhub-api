using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(int userId);
    }
}
