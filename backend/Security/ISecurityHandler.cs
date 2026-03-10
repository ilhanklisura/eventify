namespace Eventify.Backend.Security;

/// <summary>Provjera prava za akciju (npr. za Service.HasRight).</summary>
public interface ISecurityHandler
{
    bool HasRight(string action);
}
