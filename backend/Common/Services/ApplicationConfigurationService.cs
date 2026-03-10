namespace Eventify.Backend.Common.Services;

using Eventify.Backend.Models.Option;
using Eventify.Backend.Models.Response.ApplicationConfiguration;
using Eventify.Backend.Services;
using Microsoft.Extensions.Options;

/// <summary>Vraća konfiguraciju aplikacije za frontend ili druge servise (bez connection stringova, tajni, itd.).</summary>
public class ApplicationConfigurationService : IApplicationConfigurationService
{
    private readonly AppOptions _options;

    public ApplicationConfigurationService(IOptions<AppOptions> options)
    {
        _options = options.Value;
    }

    public ApplicationConfigurationModel GetApplicationConfiguration()
    {
        return new ApplicationConfigurationModel
        {
            AppName = "Eventify",
            ApiVersion = "1",
            TokenIssuer = _options.TokenOptions.Issuer
        };
    }
}
