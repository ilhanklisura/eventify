namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Ticket : BaseModel
{
    public int Id { get; set; }

    public int EventId { get; set; }
    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }

    [Required, MaxLength(20)]
    public string Status { get; set; } = "Available"; // Available | Sold | Cancelled
}
