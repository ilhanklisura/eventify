namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Response.Permission;
using Eventify.Backend.Services.Result;

public interface IPermissionService : IService
{
    ServiceResult<PermissionListModel> GetPermissionsByUserName(string userName);
    ServiceResult<PermissionListModel> GetAllPermissions();
}
