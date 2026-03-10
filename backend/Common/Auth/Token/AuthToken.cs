using System.Security.Claims;

namespace Eventify.Backend.Common.Auth.Token;

public class AuthToken
{
    public required ClaimsIdentity Subject { get; set; }
    public required string Issuer { get; set; }
    public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
}
