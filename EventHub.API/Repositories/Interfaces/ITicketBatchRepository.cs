using EventHub.API.Entities;

namespace EventHub.API.Repositories.Interfaces
{
    public interface ITicketBatchRepository
    {
        Task<IEnumerable<TicketBatch>> GetAllAsync();
        Task<TicketBatch?> GetByIdAsync(int id);
        Task AddAsync(TicketBatch ticketBatch);
        void Update(TicketBatch ticketBatch);
        void Delete(TicketBatch ticketBatch);
        Task SaveChangesAsync();

    }
}
