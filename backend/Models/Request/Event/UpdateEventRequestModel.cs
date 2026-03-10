namespace Eventify.Backend.Models.Request.Event;

using System.ComponentModel.DataAnnotations;

public class UpdateEventRequestModel
{
    public int Id { get; set; }
    [Required, MaxLength(255)]
    public string Title { get; set; } = null!;
    [Required]
    public string Description { get; set; } = null!;
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public int VenueId { get; set; }
}
