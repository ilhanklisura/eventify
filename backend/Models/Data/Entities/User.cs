namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class User : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(150)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(255)]
    public string Password { get; set; } = null!;

    [Required, MaxLength(20)]
    public string Role { get; set; } = "attendee"; // organizer | attendee (display / primary)

    public DateTime? LastLoginAt { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}
