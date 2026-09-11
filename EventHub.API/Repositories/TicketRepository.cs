using EventHub.API.Data;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.API.Repositories
{
    public class TicketRepository : ITicketRepository
    {
        private readonly AppDbContext _context;

        public TicketRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetByUserIdAsync(int userId)
        {
            return await _context.Tickets.Where(ticket => ticket.Order.UserId == userId).ToListAsync();
        }

        public async Task AddAsync(Ticket ticket)
        {
            await _context.Tickets.AddAsync(ticket);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
