namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public enum PermissionGroup
{
    Backend,
    Frontend
}

public class Permission : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;

    public PermissionGroup Group { get; set; }

    [MaxLength(64)]
    public string? DisplayName { get; set; }

    [MaxLength(256)]
    public string? Description { get; set; }

    public ICollection<RolePermission>? RolePermissions { get; set; }
}
