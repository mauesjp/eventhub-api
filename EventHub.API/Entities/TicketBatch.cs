namespace EventHub.API.Entities
{
    public class TicketBatch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; } = null!;
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        public TicketBatch()
        {
        }

        public TicketBatch(string name, decimal price, int quantity, DateTime startDate, DateTime endDate, int eventId)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            StartDate = startDate;
            EndDate = endDate;
            EventId = eventId;
        }
    }
}
