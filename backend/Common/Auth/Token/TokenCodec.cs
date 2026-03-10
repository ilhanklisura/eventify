namespace Eventify.Backend.Common.Auth.Token;

using System.IO.Compression;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class TokenCodec
{
    private class WireToken
    {
        [JsonPropertyName("sub")] public required Dictionary<string, string[]> Subject { get; set; }
        [JsonPropertyName("iss")] public required string Issuer { get; set; }
        [JsonPropertyName("iat")] public long Iat { get; set; }
        [JsonPropertyName("exp")] public long Exp { get; set; }
    }

    private readonly byte[] _key;

    public TokenCodec(string signingKey)
    {
        _key = Encoding.UTF8.GetBytes(signingKey);
    }

    public string Encode(AuthToken token)
    {
        var wire = new WireToken
        {
            Subject = ClaimsToDict(token.Subject),
            Issuer = token.Issuer,
            Iat = new DateTimeOffset(token.IssuedAt).ToUnixTimeSeconds(),
            Exp = new DateTimeOffset(token.ExpiresAt).ToUnixTimeSeconds()
        };
        var json = JsonSerializer.Serialize(wire);
        var payload = Base64Url(Compress(Encoding.UTF8.GetBytes(json)));
        var sig = Base64Url(Hmac(payload));
        return $"{payload}.{sig}";
    }

    public AuthToken? Decode(string input)
    {
        var parts = input.Split('.');
        if (parts.Length != 2) return null;
        var payload = parts[0];
        var sig = parts[1];
        if (Base64Url(Hmac(payload)) != sig) return null;
        var bytes = Decompress(Base64UrlDecode(payload));
        var wire = JsonSerializer.Deserialize<WireToken>(Encoding.UTF8.GetString(bytes));
        if (wire == null) return null;
        return new AuthToken
        {
            Subject = DictToClaims(wire.Subject),
            Issuer = wire.Issuer,
            IssuedAt = DateTimeOffset.FromUnixTimeSeconds(wire.Iat).UtcDateTime,
            ExpiresAt = DateTimeOffset.FromUnixTimeSeconds(wire.Exp).UtcDateTime
        };
    }

    private static Dictionary<string, string[]> ClaimsToDict(ClaimsIdentity identity)
    {
        var d = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase);
        foreach (var c in identity.Claims.GroupBy(x => x.Type))
            d[c.Key] = c.Select(x => x.Value).ToArray();
        return d;
    }

    private static ClaimsIdentity DictToClaims(Dictionary<string, string[]> dict)
    {
        var id = new ClaimsIdentity();
        foreach (var kv in dict ?? new Dictionary<string, string[]>())
            foreach (var v in kv.Value)
                id.AddClaim(new Claim(kv.Key, v));
        return id;
    }

    private static byte[] Compress(byte[] data)
    {
        using var outMs = new MemoryStream();
        using (var gz = new BrotliStream(outMs, CompressionLevel.SmallestSize))
            gz.Write(data, 0, data.Length);
        return outMs.ToArray();
    }

    private static byte[] Decompress(byte[] data)
    {
        using var inMs = new MemoryStream(data);
        using var outMs = new MemoryStream();
        using (var gz = new BrotliStream(inMs, CompressionMode.Decompress))
            gz.CopyTo(outMs);
        return outMs.ToArray();
    }

    private byte[] Hmac(string payload)
    {
        using var hmac = new HMACSHA256(_key);
        return hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
    }

    private static string Base64Url(byte[] bytes)
    {
        var s = Convert.ToBase64String(bytes);
        return s.Replace('+', '-').Replace('/', '_').TrimEnd('=');
    }

    private static byte[] Base64UrlDecode(string s)
    {
        s = s.Replace('-', '+').Replace('_', '/');
        switch (s.Length % 4) { case 2: s += "=="; break; case 3: s += "="; break; }
        return Convert.FromBase64String(s);
    }
}
