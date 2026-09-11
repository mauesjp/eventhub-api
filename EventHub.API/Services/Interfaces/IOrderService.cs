using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateAsync(int userId, CreateOrderDto dto);
        Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId);
    }
}
