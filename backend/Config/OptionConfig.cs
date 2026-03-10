namespace Eventify.Backend.Config;

using Eventify.Backend.Models.Option;

public class OptionConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddOptions()
            .Configure<AppOptions>(config.GetSection(nameof(AppOptions)));
    }
}
