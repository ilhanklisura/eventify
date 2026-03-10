namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class Venue : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(255)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Location { get; set; } = null!;
}
