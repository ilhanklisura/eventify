namespace Eventify.Backend.Models.Request.User;

using System.ComponentModel.DataAnnotations;

public class CreateUserRequestModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, EmailAddress, MaxLength(150)]
    public string Email { get; set; } = null!;

    [Required, MinLength(6)]
    public string Password { get; set; } = null!;

    [MaxLength(20)]
    public string Role { get; set; } = "attendee";
}
