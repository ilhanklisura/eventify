namespace Eventify.Backend.Services.Default;

using System.Security.Claims;
using Eventify.Backend.Common.Auth.Token;
using Eventify.Backend.Models.Option;
using Eventify.Backend.Models.Response.Permission;
using Eventify.Backend.Models.Response.Token;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services.Result;
using Microsoft.Extensions.Options;

public class TokenService : Service, ITokenService
{
    private readonly TokenOptions _options;

    public TokenService(IServiceProvider sp, IOptions<AppOptions> options) : base(sp)
    {
        _options = options.Value.TokenOptions;
    }

    public ServiceResult<TokenModel> CreateToken(string email)
    {
        var userService = ServiceProvider.GetRequiredService<IUserService>();
        var user = userService.GetByEmail(email);
        if (!user.IsOk) return MissingEntity("User");

        var permissionService = ServiceProvider.GetRequiredService<IPermissionService>();
        var permissions = permissionService.GetPermissionsByUserName(user.Value!.Name);

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Name, user.Value.Name));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Value.Email));
        identity.AddClaim(new Claim(ClaimTypes.Role, user.Value.Role));
        if (permissions.IsOk && permissions.Value?.Items != null)
            foreach (var p in permissions.Value.Items)
                identity.AddClaim(new Claim("permission", p.Name));

        var expiresAt = DateTime.UtcNow.AddHours(_options.TokenDurationHours);
        var codec = new TokenCodec(_options.SigningKey);
        var tokenString = codec.Encode(new AuthToken
        {
            Subject = identity,
            Issuer = _options.Issuer,
            ExpiresAt = expiresAt
        });

        return Ok(new TokenModel { Value = tokenString, User = user.Value, Permissions = permissions.IsOk ? permissions.Value : null });
    }

    public ServiceResult<ClaimsIdentity> ValidateToken(string input)
    {
        var codec = new TokenCodec(_options.SigningKey);
        var token = codec.Decode(input);
        if (token == null) return NotFound();
        if (token.ExpiresAt < DateTime.UtcNow) return ValidationError("Token expired");
        if (token.Issuer != _options.Issuer) return ValidationError("Invalid issuer");
        return Ok(token.Subject);
    }
}
