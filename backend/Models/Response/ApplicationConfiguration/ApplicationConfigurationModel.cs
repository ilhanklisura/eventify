namespace Eventify.Backend.Models.Response.ApplicationConfiguration;

/// <summary>Konfiguracija aplikacije izložena servisima / frontendu (bez osjetljivih podataka).</summary>
public class ApplicationConfigurationModel
{
    public string AppName { get; set; } = "Eventify";
    public string ApiVersion { get; set; } = "1";
    public string TokenIssuer { get; set; } = "eventify_api";
}
