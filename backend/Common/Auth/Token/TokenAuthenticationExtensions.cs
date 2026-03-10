namespace Microsoft.AspNetCore.Authentication;

using Eventify.Backend.Common.Auth.Token;

public static class TokenAuthenticationExtensions
{
    public static AuthenticationBuilder AddTokenAuthentication(this AuthenticationBuilder builder,
        Action<TokenAuthenticationOptions>? configure = null)
    {
        return builder.AddScheme<TokenAuthenticationOptions, TokenAuthenticationHandler>(
            TokenAuthenticationHandler.AuthenticationName, configure);
    }
}
