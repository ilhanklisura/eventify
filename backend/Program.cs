using Eventify.Backend.Config;

var builder = WebApplication.CreateBuilder(args);

builder
    .AddServiceConfig<OptionConfig>()
    .AddServiceConfig<DataConfig>()
    .AddServiceConfig<MvcConfig>()
    .AddServiceConfig<SwaggerConfig>()
    .AddServiceConfig<AuthConfig>()
    .AddServiceConfig<DependencyConfig>();

var app = builder.Build();

app
    .AddAppConfig<DataConfig>()
    .AddAppConfig<MvcConfig>()
    .AddAppConfig<SwaggerConfig>();

app.Run();
