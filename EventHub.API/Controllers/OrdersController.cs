using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventHub.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> Create(CreateOrderDto dto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            
            if(!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            var orderResponse = await _orderService.CreateAsync(userId, dto);

            return Created(string.Empty, orderResponse);
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetMyOrdersAsync()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(!int.TryParse(userIdString, out int userId))
            {
                return Unauthorized();
            }

            return Ok(await _orderService.GetMyOrdersAsync(userId));
        }

    }
}
