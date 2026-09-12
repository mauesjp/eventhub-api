using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface IEventService
    {
        Task<PagedResponseDto<EventResponseDto>> GetAllAsync(int pageNumber, int pageSize);
        Task<EventResponseDto?> GetByIdAsync(int id);
        Task<EventResponseDto> CreateAsync(CreateEventDto dto);
        Task<bool> UpdateAsync(int id, UpdateEventDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
