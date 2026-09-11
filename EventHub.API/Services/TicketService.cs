using EventHub.API.DTOs;
using EventHub.API.Entities;
using EventHub.API.Repositories.Interfaces;
using EventHub.API.Services.Interfaces;

namespace EventHub.API.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _repository;

        public TicketService(ITicketRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TicketResponseDto>> GetMyTicketsAsync(int userId)
        {
            var tickets = await _repository.GetByUserIdAsync(userId);
            var ticketList = new List<TicketResponseDto>();

            foreach (Ticket ticket in tickets)
            {
                var ticketResponse = new TicketResponseDto
                {
                    Id = ticket.Id,
                    Code = ticket.Code,
                    CreatedAt = ticket.CreatedAt,
                    IsUsed = ticket.IsUsed,
                    OrderId = ticket.OrderId,
                    TicketBatchId = ticket.TicketBatchId
                };

                ticketList.Add(ticketResponse);
            }

            return ticketList;
        }

        public async Task<bool> CheckInAsync(string code)
        {
            var ticket = await _repository.GetByCodeAsync(code);

            if(ticket == null)
            {
                throw new InvalidOperationException("Ticket does not exist");
            }

            if(ticket.IsUsed == true)
            {
                throw new InvalidOperationException("Ticket has already been used.");
            }

            ticket.IsUsed = true;

            _repository.Update(ticket);
            await _repository.SaveChangesAsync();

            return true;
        }
    }
}
