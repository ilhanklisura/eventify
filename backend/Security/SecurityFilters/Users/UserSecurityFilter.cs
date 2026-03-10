namespace Eventify.Backend.Security.SecurityFilters.Users;

using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Security.Database;
using Eventify.Backend.Services;

internal class UserSecurityFilter : SecurityFilter<User>
{
    private readonly IIdentityService _identityService;

    public UserSecurityFilter(IIdentityService identityService) => _identityService = identityService;

    public override IQueryable<User> Secure(IQueryable<User> query, SecurityLevel securityLevel)
    {
        var userName = _identityService.GetCurrentUserName();
        if (!userName.IsOk) return query;
        return query;
    }
}
