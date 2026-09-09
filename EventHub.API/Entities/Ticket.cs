using System.ComponentModel.DataAnnotations;

namespace EventHub.API.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TicketBatchId { get; set; }

        [MaxLength(100)]
        public string Code { get; set; }
        public bool IsUsed { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Order Order { get; set; } = null!;
        public TicketBatch TicketBatch { get; set; } = null!;

        public Ticket(int orderId, int ticketBatchId, string code)
        {
            OrderId = orderId;
            TicketBatchId = ticketBatchId;
            Code = code;
        }
    }
}
