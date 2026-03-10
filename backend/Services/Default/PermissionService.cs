namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Response.Permission;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class PermissionService : Service, IPermissionService
{
    private readonly DataContext _db;

    public PermissionService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public ServiceResult<PermissionListModel> GetPermissionsByUserName(string userName)
    {
        var user = _db.Users.AsNoTracking().FirstOrDefault(u => u.Name == userName || u.Email == userName);
        if (user == null) return NotFound();

        var permissionIds = _db.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == user.Id)
            .Join(_db.RolePermissions.AsNoTracking(), ur => ur.RoleId, rp => rp.RoleId, (ur, rp) => rp.PermissionId)
            .Distinct()
            .ToList();

        var permissions = _db.Permissions.AsNoTracking()
            .Where(p => permissionIds.Contains(p.Id))
            .Select(p => new PermissionListModelItem
            {
                Id = p.Id,
                Name = p.Name,
                Group = p.Group.ToString(),
                DisplayName = p.DisplayName
            })
            .ToList();

        return Ok(new PermissionListModel(permissions));
    }

    public ServiceResult<PermissionListModel> GetAllPermissions()
    {
        var items = _db.Permissions.AsNoTracking()
            .OrderBy(p => p.Group).ThenBy(p => p.Name)
            .Select(p => new PermissionListModelItem { Id = p.Id, Name = p.Name, Group = p.Group.ToString(), DisplayName = p.DisplayName })
            .ToList();
        return Ok(new PermissionListModel(items));
    }
}
