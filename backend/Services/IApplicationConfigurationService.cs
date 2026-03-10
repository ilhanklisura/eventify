namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Response.ApplicationConfiguration;

/// <summary>Servis za dohvat konfiguracije aplikacije (bez osjetljivih podataka).</summary>
public interface IApplicationConfigurationService : IService
{
    ApplicationConfigurationModel GetApplicationConfiguration();
}
