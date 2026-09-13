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
        [ProducesResponseType(typeof(IEnumerable<TicketBatchResponseDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TicketBatchResponseDto>>> GetAll()
        {
            return Ok(await _ticketBatchService.GetAllAsync());
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TicketBatchResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(typeof(TicketBatchResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TicketBatchResponseDto>> Create(CreateTicketBatchDto dto)
        {
            var ticketBatch = await _ticketBatchService.CreateAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = ticketBatch.Id }, ticketBatch);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
