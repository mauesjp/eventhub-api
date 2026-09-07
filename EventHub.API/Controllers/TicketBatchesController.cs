using EventHub.API.DTOs;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketBatchesController : ControllerBase
    {
        private readonly ITicketBatchService _ticketBatchService;

        public TicketBatchesController(ITicketBatchService ticketBatchService)
        {
            _ticketBatchService = ticketBatchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketBatchResponseDto>>> GetAll()
        {
            return Ok(await _ticketBatchService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketBatchResponseDto>> GetById(int id)
        {
            var ticketBatch = await _ticketBatchService.GetByIdAsync(id);

            if(ticketBatch == null)
            {
                return NotFound();
            }

            return Ok(ticketBatch);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<TicketBatchResponseDto>> Create(CreateTicketBatchDto dto)
        {
            var ticketBatch = await _ticketBatchService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = ticketBatch.Id }, ticketBatch);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UpdateTicketBatchDto dto)
        {
            var updateEvent = await _ticketBatchService.UpdateAsync(id, dto);

            if (!updateEvent)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleteEvent = await _ticketBatchService.DeleteAsync(id);

            if (!deleteEvent)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
