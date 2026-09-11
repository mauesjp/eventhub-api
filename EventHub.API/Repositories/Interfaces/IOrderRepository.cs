using EventHub.API.Entities;

namespace EventHub.API.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByUserIdAsync(int userId);
        Task AddAsync(Order order);
        Task SaveChangesAsync();
    }
}
