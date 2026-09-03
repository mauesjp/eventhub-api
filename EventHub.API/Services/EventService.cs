using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;

namespace EventHub.API.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<IEnumerable<EventResponseDto>> GetAllAsync()
        {
            var events = await _eventRepository.GetAllAsync();
            var eventsResponse = new List<EventResponseDto>();

            foreach (Event item in events)
            {
                var eventResponse = new EventResponseDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Date = item.Date,
                    Location = item.Location,
                    Capacity = item.Capacity
                };

                eventsResponse.Add(eventResponse);
            }

            return eventsResponse;
        }

        public async Task<EventResponseDto?> GetByIdAsync(int id)
        {
            var eventItem = await _eventRepository.GetByIdAsync(id);

            if(eventItem == null)
            {
                return null;
            }

            var eventResponse = new EventResponseDto
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Description = eventItem.Description,
                Date = eventItem.Date,
                Location = eventItem.Location,
                Capacity = eventItem.Capacity
            };

            return eventResponse;
        }

        public async Task<EventResponseDto> CreateAsync(CreateEventDto dto)
        {
            Event newEvent = new Event
            (
                dto.Name,
                dto.Description,
                dto.Date,
                dto.Location,
                dto.Capacity
            );

            await _eventRepository.AddAsync(newEvent);

            await _eventRepository.SaveChangesAsync();

            EventResponseDto eventResponse = new EventResponseDto
            {
                Id = newEvent.Id,
                Name = newEvent.Name,
                Description = newEvent.Description,
                Date = newEvent.Date,
                Location = newEvent.Location,
                Capacity = newEvent.Capacity
            };

            return eventResponse;
        }

        public async Task<bool> UpdateAsync(int id, UpdateEventDto dto)
        {
            var eventItem = await _eventRepository.GetByIdAsync(id);

            if(eventItem == null)
            {
                return false;
            }

            eventItem.Name = dto.Name;
            eventItem.Description = dto.Description;
            eventItem.Date = dto.Date;
            eventItem.Location = dto.Location;
            eventItem.Capacity = dto.Capacity;

            _eventRepository.Update(eventItem);
            await _eventRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync (int id)
        {
            var eventItem = await _eventRepository.GetByIdAsync(id);

            if(eventItem == null)
            {
                return false;
            }

            _eventRepository.Delete(eventItem);

            await _eventRepository.SaveChangesAsync();

            return true;
        }
    }
}
