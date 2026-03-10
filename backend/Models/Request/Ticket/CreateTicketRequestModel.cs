namespace Eventify.Backend.Models.Request.Ticket;

using System.ComponentModel.DataAnnotations;

public class CreateTicketRequestModel
{
    public int EventId { get; set; }
    public int UserId { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [MaxLength(20)]
    public string Status { get; set; } = "Available";
}
