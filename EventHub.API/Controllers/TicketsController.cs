using EventHub.API.DTOs;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<TicketResponseDto>>> GetMyTicketsAsync()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            return Ok(await _ticketService.GetMyTicketsAsync(userId));
        }
    }
}
