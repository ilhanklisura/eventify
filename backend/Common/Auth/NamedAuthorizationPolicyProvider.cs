namespace Eventify.Backend.Common.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

/// <summary>Policy provider za imenovane policyje – npr. "claim:permission:event_create" zahtijeva claim "permission" s vrijednošću "event_create".</summary>
public class NamedAuthorizationPolicyProvider : IAuthorizationPolicyProvider
{
    private readonly DefaultAuthorizationPolicyProvider _defaultProvider;

    public bool AllowsCachingPolicies => true;

    public NamedAuthorizationPolicyProvider(IOptions<AuthorizationOptions> authorizationOptions)
    {
        _defaultProvider = new DefaultAuthorizationPolicyProvider(authorizationOptions);
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => _defaultProvider.GetDefaultPolicyAsync();
    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() => _defaultProvider.GetFallbackPolicyAsync();

    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var parts = policyName.Split(':', StringSplitOptions.TrimEntries);
        var policy = GetNamedPolicy(parts);
        if (policy != null) return Task.FromResult<AuthorizationPolicy?>(policy);
        return _defaultProvider.GetPolicyAsync(policyName);
    }

    private static AuthorizationPolicy? GetNamedPolicy(string[] parts)
    {
        if (parts.Length < 2) return null;
        var type = parts[0].ToLowerInvariant();
        var args = parts.Skip(1).ToArray();
        return type switch
        {
            "claim" => GetClaimPolicy(args),
            _ => null
        };
    }

    /// <summary>claim:permission:event_create ili claim:event_create (default type "permission").</summary>
    private static AuthorizationPolicy? GetClaimPolicy(string[] args)
    {
        string claimType;
        string[] values;
        if (args.Length >= 2) { claimType = args[0]; values = args.Skip(1).ToArray(); }
        else if (args.Length == 1) { claimType = "permission"; values = new[] { args[0] }; }
        else return null;
        var builder = new AuthorizationPolicyBuilder();
        builder.RequireAuthenticatedUser();
        builder.RequireClaim(claimType, values);
        return builder.Build();
    }
}
