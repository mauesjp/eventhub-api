using EventHub.API.DTOs;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {

        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventResponseDto>>> GetAll()
        {
            var events = await _eventService.GetAllAsync();

            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponseDto>> GetById(int id)
        {
            var eventItem = await _eventService.GetByIdAsync(id);

            if (eventItem == null)
            {
                return NotFound();
            }

            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<ActionResult<EventResponseDto>> Create(CreateEventDto eventItem)
        {
           var eventResponse = await _eventService.CreateAsync(eventItem);

            return CreatedAtAction(nameof(GetById), new { id = eventResponse.Id }, eventResponse);
        }

        [HttpPut]
        public async Task<ActionResult> Update(int id, UpdateEventDto eventItem)
        {
            var updateEvent = await _eventService.UpdateAsync(id, eventItem);

            if(!updateEvent)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEvent = await _eventService.DeleteAsync(id);

            if(!deleteEvent)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
