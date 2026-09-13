namespace EventHub.API.DTOs
{
    public class TicketResponseDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int TicketBatchId { get; set; }
        public string Code { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
