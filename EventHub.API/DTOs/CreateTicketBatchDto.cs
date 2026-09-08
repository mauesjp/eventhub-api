using System.ComponentModel.DataAnnotations;

namespace EventHub.API.DTOs
{
    public class CreateTicketBatchDto
    {
        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Range(1, int.MaxValue)]
        public int EventId { get; set; }
    }
}
