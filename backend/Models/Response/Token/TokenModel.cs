namespace Eventify.Backend.Models.Response.Token;

using Eventify.Backend.Models.Response.Permission;
using Eventify.Backend.Models.Response.User;

public class TokenModel
{
    public required string Value { get; set; }
    public required UserModel User { get; set; }
    public PermissionListModel? Permissions { get; set; }
}
