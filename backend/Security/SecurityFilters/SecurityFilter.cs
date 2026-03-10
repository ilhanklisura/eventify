namespace Eventify.Backend.Security.SecurityFilters;

using Eventify.Backend.Security.Database;

/// <summary>Bazna klasa za security filter – Secure() i pomoć SendEmpty().</summary>
public abstract class SecurityFilter<T> : ISecurityFilter<T>
{
    public abstract IQueryable<T> Secure(IQueryable<T> query, SecurityLevel securityLevel);

    protected static IQueryable<T> SendEmpty(IQueryable<T> query) => Array.Empty<T>().AsQueryable();
}
