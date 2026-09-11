using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Exceptions;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;

namespace EventHub.API.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly ITicketRepository _ticketRepository;
        private readonly ITicketBatchRepository _ticketBatch;

        public OrderService(IOrderRepository repository, ITicketBatchRepository ticketBatch, ITicketRepository ticketRepository)
        {
            _repository = repository;
            _ticketBatch = ticketBatch;
            _ticketRepository = ticketRepository;
        }

        public async Task<OrderResponseDto> CreateAsync(int userId, CreateOrderDto dto)
        {
            var ticketBatch = await _ticketBatch.GetByIdAsync(dto.TicketBatchId);

            if (ticketBatch == null)
            {
                throw new NotFoundException("Non-existent Batch");
            }

            if (dto.Quantity > ticketBatch.Quantity)
            {
                throw new BusinessRuleException("The quantity exceeds the number of available tickets.");
            }

            var now = DateTime.UtcNow;

            if (now < ticketBatch.StartDate || now > ticketBatch.EndDate)
            {
                throw new BusinessRuleException("Ticket Batch is out of range");
            }

            var totalAmount = dto.Quantity * ticketBatch.Price;

            var order = new Order(userId, totalAmount, dto.TicketBatchId, dto.Quantity);

            ticketBatch.Quantity -= dto.Quantity;
            _ticketBatch.Update(ticketBatch);

            await _repository.AddAsync(order);
            await _repository.SaveChangesAsync();


            for (int i = 0; i < dto.Quantity; i++)
            {
                var code = $"EVT-{Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper()}";

                var ticket = new Ticket(order.Id, dto.TicketBatchId, code);

                await _ticketRepository.AddAsync(ticket);
            }
            await _ticketRepository.SaveChangesAsync();


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

        public async Task<IEnumerable<OrderResponseDto>> GetMyOrdersAsync(int userId)
        {
            var orders = await _repository.GetByUserIdAsync(userId);
            List<OrderResponseDto> orderList = new List<OrderResponseDto>();

            foreach (Order order in orders)
            {
                var orderResponse = new OrderResponseDto
                {
                    CreatedAt = order.CreatedAt,
                    TotalAmount = order.TotalAmount,
                    Id = order.Id,
                    Quantity = order.Quantity,
                    Status = order.Status,
                    TicketBatchId = order.TicketBatchId,
                    UserId = order.UserId
                };

                orderList.Add(orderResponse);
            }

            return orderList;
        }
    }
}
