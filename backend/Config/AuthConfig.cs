namespace Eventify.Backend.Config;

using Eventify.Backend.Common.Auth;
using Eventify.Backend.Common.Auth.Hash;
using Eventify.Backend.Common.Auth.Token;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

public class AuthConfig : IConfig
{
    public void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IHashProvider, BcryptHashProvider>();
        services.AddAuthentication(TokenAuthenticationHandler.AuthenticationName)
            .AddTokenAuthentication();
        services.AddSingleton<IAuthorizationPolicyProvider, NamedAuthorizationPolicyProvider>();
    }
}
