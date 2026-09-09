using System.ComponentModel.DataAnnotations;

namespace EventHub.API.DTOs
{
    public class CreateOrderDto
    {
        [Range(1, int.MaxValue)]
        public int TicketBatchId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}
