using EventHub.API.Entities;

namespace EventHub.API.Repositories.Interfaces
{
    public interface IEventRepository
    {
        Task<IEnumerable<Event>> GetAllAsync(int pageNumber, int pageSize);
        Task<Event?> GetByIdAsync(int id);
        Task AddAsync(Event newEvent);
        void Update(Event eventItem);
        void Delete(Event eventItem);
        Task SaveChangesAsync();
        Task<int> CountAsync();
    }
}
