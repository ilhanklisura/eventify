namespace Eventify.Backend.Config;

public interface IConfig
{
    void ConfigureServices(IServiceCollection services, IConfiguration config) { }

    void ConfigureApp(WebApplication app) { }
}
