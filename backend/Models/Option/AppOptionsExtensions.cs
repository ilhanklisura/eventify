namespace Microsoft.Extensions.Configuration;

using Eventify.Backend.Models.Option;

public static class AppOptionsExtensions
{
    public static AppOptions GetOptions(this IConfiguration configuration)
    {
        var options = new AppOptions();
        configuration.GetSection(nameof(AppOptions)).Bind(options);
        return options;
    }
}
