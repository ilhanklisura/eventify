namespace Eventify.Backend.Models.Request.Venue;

using System.ComponentModel.DataAnnotations;

public class CreateVenueRequestModel
{
    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Location { get; set; } = null!;
}
