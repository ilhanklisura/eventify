namespace Eventify.Backend.Config;

using Eventify.Backend.Mapping;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Security.Database;
using Eventify.Backend.Security.SecurityFilters.Users;
using Eventify.Backend.Services;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

public class DependencyConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.Scan(scan => scan.FromAssemblyOf<Program>()
            .AddClasses(classes => classes.AssignableTo<IService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(IMapper<,>)))
            .AsImplementedInterfaces()
            .WithTransientLifetime()

            .AddClasses(classes => classes.AssignableTo(typeof(ISecurityFilter<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        services.AddScoped<ISecurityFilter<User>, UserSecurityFilter>();
    }
}
