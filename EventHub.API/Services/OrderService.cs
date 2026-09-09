using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;

namespace EventHub.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ITicketBatchRepository _ticketBatch;

        public OrderService(IOrderRepository repository, ITicketBatchRepository ticketBatch)
        {
            _repository = repository;
            _ticketBatch = ticketBatch;
        }

        public async Task<OrderResponseDto> CreateAsync(int userId, CreateOrderDto dto)
        {
            var ticketBatch = await _ticketBatch.GetByIdAsync(dto.TicketBatchId);

            if(ticketBatch == null)
            {
                throw new InvalidOperationException("Non-existent Batch");
            }

            if(dto.Quantity > ticketBatch.Quantity)
            {
                throw new InvalidOperationException("The quantity exceeds the number of available tickets.");
            }

            var now = DateTime.UtcNow;

            if(now < ticketBatch.StartDate || now > ticketBatch.EndDate)
            {
                throw new InvalidOperationException("Ticket Batch is out of range");
            }

            var totalAmount = dto.Quantity * ticketBatch.Price;

            var order = new Order(userId, totalAmount, dto.TicketBatchId, dto.Quantity);

            ticketBatch.Quantity -= dto.Quantity;
            _ticketBatch.Update(ticketBatch);

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();


            var orderResponse = new OrderResponseDto
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                CreatedAt = order.CreatedAt,
                Quantity = order.Quantity,
                TotalAmount = order.TotalAmount,
                TicketBatchId = order.TicketBatchId
            };

            return orderResponse;
        }
    }
}
