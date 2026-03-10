namespace Eventify.Backend.Common.Extensions;

public static class RequestExtensions
{
    /// <summary>Client IP: X-Forwarded-For, X-Real-Ip, or RemoteIpAddress (kad je app iza proxyja).</summary>
    public static string? GetIpAddress(this HttpRequest request)
    {
        if (request.Headers.TryGetValue("X-Forwarded-For", out var forwarded))
            return forwarded.ToString().Split(',')[0].Trim();
        if (request.Headers.TryGetValue("X-Real-Ip", out var real))
            return real.ToString();
        return request.HttpContext.Connection.RemoteIpAddress?.ToString();
    }

    /// <summary>Scheme (http/https): X-Forwarded-Proto ili request.Scheme (kad je app iza proxyja).</summary>
    public static string GetScheme(this HttpRequest request)
    {
        if (request.Headers.TryGetValue("X-Forwarded-Proto", out var forwarded))
            return forwarded.ToString();
        return request.Scheme;
    }
}
