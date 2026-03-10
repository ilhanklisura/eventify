namespace Eventify.Backend.Config;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Option;
using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class DataConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        var conn = GetConnectionString(config);
        if (!conn.Type.Equals("sqlserver", StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Eventify backend expects SQL Server. Set ConnectionStrings:Strings:0:Type to sqlserver.");

        services.AddDbContext<DataContext>((sp, opts) => opts.UseSqlServer(conn.Value));

        var descriptor = services.Single(d => d.ServiceType == typeof(DataContext));
        services.Remove(descriptor);
        services.AddScoped<DataContext>(sp => new DataContext(sp.GetRequiredService<DbContextOptions<DataContext>>(), sp));

        services
            .AddFluentMigratorCore()
            .ConfigureRunner(r => ConfigureMigrations(r, conn));
    }

    public void ConfigureApp(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
        runner.MigrateUp();
    }

    private static ConnectionString GetConnectionString(IConfiguration config)
    {
        var options = config.GetOptions();
        var conn = options.ConnectionStrings.GetDefault()
            ?? throw new InvalidOperationException("Missing default connection string.");
        return conn;
    }

    private static void ConfigureMigrations(IMigrationRunnerBuilder runnerBuilder, ConnectionString conn)
    {
        var assembly = typeof(DataConfig).Assembly;
        runnerBuilder.ScanIn(assembly).For.Migrations();
        runnerBuilder.AddSqlServer();
        runnerBuilder.WithGlobalConnectionString(conn.Value);
    }
}
