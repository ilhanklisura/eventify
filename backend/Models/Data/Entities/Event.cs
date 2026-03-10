namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Event : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Title { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    public DateTime Date { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }

    public int VenueId { get; set; }
    [ForeignKey(nameof(VenueId))]
    public Venue? Venue { get; set; }

    public int OrganizerId { get; set; }
    [ForeignKey(nameof(OrganizerId))]
    public User? Organizer { get; set; }
}
