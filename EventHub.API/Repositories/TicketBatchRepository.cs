using EventHub.API.Data;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.API.Repositories
{
    public class TicketBatchRepository : ITicketBatchRepository
    {
        private readonly AppDbContext _context;

        public TicketBatchRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketBatch>> GetAllAsync()
        {
            var ticketBatches = await _context.TicketBatches.ToListAsync();

            return ticketBatches;
        }

        public async Task<TicketBatch?> GetByIdAsync(int id)
        {
            return await _context.TicketBatches.FindAsync(id);
        }

        public async Task AddAsync(TicketBatch ticketBatch)
        {
            await _context.TicketBatches.AddAsync(ticketBatch);
        }

        public void Update(TicketBatch ticketBatch)
        {
            _context.TicketBatches.Update(ticketBatch);
        }

        public void Delete(TicketBatch ticketBatch)
        {
            _context.TicketBatches.Remove(ticketBatch);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
