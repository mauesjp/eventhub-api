using EventHub.API.Data;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventHub.API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly AppDbContext _context;

        public EventRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Events
                .OrderBy(e => e.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Event?> GetByIdAsync(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task AddAsync(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
        }

        public void Update(Event eventItem)
        {
            _context.Events.Update(eventItem);
        }

        public void Delete(Event eventItem)
        {
            _context.Events.Remove(eventItem);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Events.CountAsync();           
        }
    }
}
