namespace Eventify.Backend.Tests;

using System.Security.Claims;
using Eventify.Backend.Common.Auth.Token;

public class TokenCodecTests
{
    [Fact]
    public void EncodeDecode_Roundtrip_PreservesClaimsAndIssuer()
    {
        var codec = new TokenCodec("eventify_signing_key_at_least_32_characters_long");

        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.Name, "admin"));
        identity.AddClaim(new Claim(ClaimTypes.Email, "admin@example.com"));
        identity.AddClaim(new Claim("permission", "user_list"));
        identity.AddClaim(new Claim("permission", "event_create"));

        var token = new AuthToken
        {
            Subject = identity,
            Issuer = "eventify_api",
            IssuedAt = DateTime.UtcNow.AddMinutes(-1),
            ExpiresAt = DateTime.UtcNow.AddHours(1),
        };

        var encoded = codec.Encode(token);
        var decoded = codec.Decode(encoded);

        Assert.NotNull(decoded);
        Assert.Equal("eventify_api", decoded!.Issuer);

        var claims = decoded.Subject.Claims.ToList();
        Assert.Contains(claims, c => c.Type == ClaimTypes.Name && c.Value == "admin");
        Assert.Contains(claims, c => c.Type == ClaimTypes.Email && c.Value == "admin@example.com");
        Assert.Equal(2, claims.Count(c => c.Type == "permission"));
    }

    [Fact]
    public void Decode_ReturnsNull_WhenSignatureInvalid()
    {
        var codec = new TokenCodec("eventify_signing_key_at_least_32_characters_long");
        var bad = "aaaa.bbbb";
        Assert.Null(codec.Decode(bad));
    }
}

