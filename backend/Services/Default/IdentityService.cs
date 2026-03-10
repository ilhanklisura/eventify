namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Common.Extensions;
using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class IdentityService : Service, IIdentityService
{
    private readonly DataContext _context;
    private readonly IHttpContextAccessor _accessor;

    public IdentityService(IServiceProvider sp, IHttpContextAccessor accessor, DataContext context) : base(sp)
    {
        _context = context;
        _accessor = accessor;
    }

    public ServiceResult<string> GetCurrentUserName()
    {
        var user = _accessor.HttpContext?.User;
        if (user == null) return NotFound();
        var name = user.GetName();
        if (string.IsNullOrWhiteSpace(name)) return NotFound();
        return Ok(name);
    }

    public User? CurrentUser()
    {
        var name = GetCurrentUserName();
        if (!name.IsOk) return null;
        return _context.Users.AsNoTracking().FirstOrDefault(x => x.Name == name.Value || x.Email == name.Value);
    }
}
