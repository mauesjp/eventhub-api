using EventHub.API.DTOs;

namespace EventHub.API.Services.Interfaces
{
    public interface ITicketBatchService
    {
        Task<IEnumerable<TicketBatchResponseDto>> GetAllAsync();
        Task<TicketBatchResponseDto?> GetByIdAsync(int id);
        Task<TicketBatchResponseDto> CreateAsync(CreateTicketBatchDto dto);
        Task<bool> UpdateAsync(int id, UpdateTicketBatchDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
