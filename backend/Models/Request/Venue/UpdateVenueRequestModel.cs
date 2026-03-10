namespace Eventify.Backend.Models.Request.Venue;

using System.ComponentModel.DataAnnotations;

public class UpdateVenueRequestModel
{
    public int Id { get; set; }
    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;
    [Required, MaxLength(255)]
    public string Location { get; set; } = null!;
}
