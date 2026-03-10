namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class Booking : BaseModel
{
    public int Id { get; set; }

    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    public int EventId { get; set; }
    [ForeignKey(nameof(EventId))]
    public Event? Event { get; set; }
}
