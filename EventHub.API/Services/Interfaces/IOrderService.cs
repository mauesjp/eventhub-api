using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponseDto> CreateAsync(int userId, CreateOrderDto dto);
    }
}
