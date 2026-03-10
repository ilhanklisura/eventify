namespace Eventify.Backend.Config;

using Eventify.Backend.Common.Auth.Token;
using Microsoft.OpenApi.Models;

public class SwaggerConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddSwaggerGen(opts =>
        {
            var name = TokenAuthenticationHandler.AuthenticationName;
            var scheme = new OpenApiSecurityScheme
            {
                Name = TokenAuthenticationHandler.HeaderName,
                Scheme = TokenAuthenticationHandler.SchemeName,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "JWT Bearer token",
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = name }
            };
            opts.AddSecurityDefinition(name, scheme);
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, new List<string>() } });
        });
    }

    public void ConfigureApp(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}
