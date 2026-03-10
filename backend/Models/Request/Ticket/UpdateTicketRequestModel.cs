namespace Eventify.Backend.Models.Request.Ticket;

using System.ComponentModel.DataAnnotations;

public class UpdateTicketRequestModel
{
    public int Id { get; set; }
    [MaxLength(20)]
    public string Status { get; set; } = "Available";
}
