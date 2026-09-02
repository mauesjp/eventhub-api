using EventHub.API.Data;
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
        public async Task<ActionResult<IEnumerable<Event>>> GetAll()
        {
            var events = await _context.Events.ToListAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetById(int id)
        {
            var eventItem = await _context.Events.FindAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> Create(Event eventItem)
        {
            _context.Events.Add(eventItem);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = eventItem.Id }, eventItem);
        }

        [HttpPut]
        public async Task<ActionResult<Event>> Update(int id, Event eventItem)
        {
            var existingEvent = await _context.Events.FindAsync(id);

            if(existingEvent == null)
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
    }
}
