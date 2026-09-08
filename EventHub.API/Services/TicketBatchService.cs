using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;

namespace EventHub.API.Services
{
    public class TicketBatchService : ITicketBatchService
    {
        private readonly ITicketBatchRepository _ticketBatchRepository;
        private readonly IEventRepository _eventRepository;

        public TicketBatchService(ITicketBatchRepository ticketBatchRepository, IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
            _ticketBatchRepository = ticketBatchRepository;
        }

        public async Task<IEnumerable<TicketBatchResponseDto>> GetAllAsync()
        {
            var ticketBatches = await _ticketBatchRepository.GetAllAsync();

            List<TicketBatchResponseDto> ticketBatchResponseList = new List<TicketBatchResponseDto>();

            foreach (TicketBatch item in ticketBatches)
            {
                var ticketBatchResponse = new TicketBatchResponseDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Quantity = item.Quantity,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    EventId = item.EventId,
                    Price = item.Price
                };

                ticketBatchResponseList.Add(ticketBatchResponse);
            }

            return ticketBatchResponseList;
        }

        public async Task<TicketBatchResponseDto?> GetByIdAsync(int id)
        {
            var ticketBatch = await _ticketBatchRepository.GetByIdAsync(id);

            if (ticketBatch == null)
            {
                return null;
            }

            var ticketBatchResponse = new TicketBatchResponseDto
            {
                Id = ticketBatch.Id,
                Name = ticketBatch.Name,
                Price = ticketBatch.Price,
                Quantity = ticketBatch.Quantity,
                StartDate = ticketBatch.StartDate,
                EndDate = ticketBatch.EndDate,
                EventId = ticketBatch.EventId
            };

            return ticketBatchResponse;
        }

        public async Task<TicketBatchResponseDto> CreateAsync(CreateTicketBatchDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
            {
                throw new InvalidOperationException("EndDate must be greater than StartDate.");
            }

            if (dto.Price <= 0)
            {
                throw new InvalidOperationException("Price must be greater than zero.");
            }

            if (dto.Quantity <= 0)
            {
                throw new InvalidOperationException("Quantity must be greater than zero.");
            }

            var findEvent = await _eventRepository.GetByIdAsync(dto.EventId);

            if (findEvent == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            var newTicketBatch = new TicketBatch
                (dto.Name,
                dto.Price,
                dto.Quantity,
                dto.StartDate,
                dto.EndDate,
                dto.EventId);

            await _ticketBatchRepository.AddAsync(newTicketBatch);
            await _ticketBatchRepository.SaveChangesAsync();

            var ticketBatchResponse = new TicketBatchResponseDto
            {
                Id = newTicketBatch.Id,
                Name = newTicketBatch.Name,
                Price = newTicketBatch.Price,
                Quantity = newTicketBatch.Quantity,
                StartDate = newTicketBatch.StartDate,
                EndDate = newTicketBatch.EndDate,
                EventId = newTicketBatch.EventId
            };

            return ticketBatchResponse;
        }

        public async Task<bool> UpdateAsync(int id, UpdateTicketBatchDto dto)
        {
            if (dto.EndDate <= dto.StartDate)
            {
                throw new InvalidOperationException("EndDate must be greater than StartDate.");
            }

            if (dto.Price <= 0)
            {
                throw new InvalidOperationException("Price must be greater than zero.");
            }

            if (dto.Quantity <= 0)
            {
                throw new InvalidOperationException("Quantity must be greater than zero.");
            }

            var ticketBatch = await _ticketBatchRepository.GetByIdAsync(id);

            if (ticketBatch == null)
            {
                return false;
            }

            var findEvent = await _eventRepository.GetByIdAsync(dto.EventId);

            if (findEvent == null)
            {
                throw new InvalidOperationException("Event not found.");
            }

            ticketBatch.Name = dto.Name;
            ticketBatch.Price = dto.Price;
            ticketBatch.Quantity = dto.Quantity;
            ticketBatch.StartDate = dto.StartDate;
            ticketBatch.EndDate = dto.EndDate;
            ticketBatch.EventId = dto.EventId;

            _ticketBatchRepository.Update(ticketBatch);
            await _ticketBatchRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var ticketBatch = await _ticketBatchRepository.GetByIdAsync(id);

            if (ticketBatch == null)
            {
                return false;
            }

            _ticketBatchRepository.Delete(ticketBatch);
            await _ticketBatchRepository.SaveChangesAsync();

            return true;
        }
    }
}
