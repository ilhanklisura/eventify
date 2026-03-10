namespace Eventify.Backend.Common.Auth.Token;

using System.Security.Claims;
using System.Text.Encodings.Web;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

public class TokenAuthenticationOptions : AuthenticationSchemeOptions { }

public class TokenAuthenticationHandler : AuthenticationHandler<TokenAuthenticationOptions>
{
    public const string AuthenticationName = "TokenAuthentication";
    public const string HeaderName = "Authorization";
    public const string SchemeName = "Bearer";

    private readonly IServiceProvider _sp;

    public TokenAuthenticationHandler(
        IOptionsMonitor<TokenAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IServiceProvider sp)
        : base(options, logger, encoder)
    {
        _sp = sp;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.TryGetValue(HeaderName, out var values))
            return Task.FromResult(AuthenticateResult.NoResult());
        var header = values.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(header) || !header.StartsWith(SchemeName, StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.NoResult());
        var token = header.Substring(SchemeName.Length).Trim();
        var tokenService = _sp.GetRequiredService<ITokenService>();
        var result = tokenService.ValidateToken(token);
        if (!result.IsOk)
            return Task.FromResult(AuthenticateResult.NoResult());
        var identity = new ClaimsIdentity(result.Value!.Claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, Scheme.Name)));
    }
}
