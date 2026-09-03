using EventHub.API.Data;
using EventHub.API.DTOs;
using EventHub.API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly AppDbContext _context;

        public EventsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            var eventsResponse = new List<EventResponseDto>();

            foreach (Event item in events)
            {
                var newevent = new EventResponseDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Date = item.Date,
                    Location = item.Location,
                    Capacity = item.Capacity
                };

                eventsResponse.Add(newevent);
            }

            return Ok(eventsResponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponseDto>> GetById(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            EventResponseDto eventResponse = new EventResponseDto
            {
                Id = eventItem.Id,
                Name = eventItem.Name,
                Description = eventItem.Description,
                Date = eventItem.Date,
                Location = eventItem.Location,
                Capacity = eventItem.Capacity
            };

            return Ok(eventResponse);
        }

        [HttpPost]
        public async Task<ActionResult<EventResponseDto>> Create(CreateEventDto eventItem)
        {
            Event newEvent = new Event(eventItem.Name, eventItem.Description, eventItem.Date, eventItem.Location, eventItem.Capacity);
            _context.Events.Add(newEvent);

            await _context.SaveChangesAsync();

            EventResponseDto eventResponse = new EventResponseDto
            {
                Id = newEvent.Id,
                Name = newEvent.Name,
                Description = newEvent.Description,
                Date = newEvent.Date,
                Location = newEvent.Location,
                Capacity = newEvent.Capacity
            };

            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, eventResponse);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateEventDto eventItem)
        {
            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            existingEvent.Name = eventItem.Name;
            existingEvent.Description = eventItem.Description;
            existingEvent.Date = eventItem.Date;
            existingEvent.Location = eventItem.Location;
            existingEvent.Capacity = eventItem.Capacity;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEvent = await _context.Events.FindAsync(id);

            if (existingEvent == null)
            {
                return NotFound();
            }

            _context.Events.Remove(existingEvent);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
