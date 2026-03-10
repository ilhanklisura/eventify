namespace Eventify.Backend.Common.Extensions;

using System.Security.Claims;

public static class ClaimsExtensions
{
    public static string? GetName(this ClaimsPrincipal user) =>
        user.FindFirst(ClaimTypes.Name)?.Value ?? user.FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>Ima li korisnik permission claim s danim imenom (npr. "event_create").</summary>
    public static bool HasPermission(this ClaimsPrincipal user, string name) =>
        user.FindFirst(c => c.Type == "permission" && c.Value == name) != null;
}
