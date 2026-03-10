namespace Eventify.Backend.Security.Database;

/// <summary>Sigurnosna politika na razini upita – filtrira IQueryable po pravima.</summary>
public interface ISecurityFilter<T>
{
    IQueryable<T> Secure(IQueryable<T> query, SecurityLevel securityLevel);
}
