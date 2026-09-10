using EventHub.API.Entities;

namespace EventHub.API.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task SaveChangesAsync();
    }
}
