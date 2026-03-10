namespace Eventify.Backend.Config;

using Eventify.Backend.Hubs;

public class MvcConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSignalR();
        services.AddCors(opts =>
        {
            opts.AddPolicy("AllowAny", b =>
            {
                b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
    }

    public void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseCors("AllowAny");
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();
        app.MapHub<NotificationsHub>("/hubs/notifications");
    }
}
