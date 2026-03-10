namespace Eventify.Backend.Models.Response.Permission;

using Eventify.Backend.Models.Response.Common;

public class PermissionListModel : ListModel<PermissionListModelItem>
{
    public PermissionListModel() { }

    public PermissionListModel(IEnumerable<PermissionListModelItem> permissions) : base(permissions) { }
}

public class PermissionListModelItem
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Group { get; set; }
    public string? DisplayName { get; set; }
}
