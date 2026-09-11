using EventHub.API.Entities;

namespace EventHub.API.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task AddAsync(Ticket ticket);
        Task SaveChangesAsync();
        Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId);
        Task<Ticket?> GetByCodeAsync(string code);
        void Update(Ticket ticket);
    }
}
