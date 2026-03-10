namespace Eventify.Backend.Models.Request.Token;

using System.ComponentModel.DataAnnotations;

public class CreateTokenRequestModel
{
    [Required, EmailAddress]
    public string Email { get; set; } = null!;

    [Required, MinLength(1)]
    public string Password { get; set; } = null!;
}
