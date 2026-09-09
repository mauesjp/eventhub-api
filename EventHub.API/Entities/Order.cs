using EventHub.API.Entities.Enums;

namespace EventHub.API.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public User User { get; set; } = null!;
        public int TicketBatchId { get; set; }
        public int Quantity { get; set; }
        public TicketBatch TicketBatch { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public Order(int userId, decimal totalAmount, int ticketBatchId, int quantity)
        {
            UserId = userId;
            TotalAmount = totalAmount;
            TicketBatchId = ticketBatchId;
            Quantity = quantity;
        }

    }
}
