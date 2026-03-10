namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class Role : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(64)]
    public string Name { get; set; } = null!;

    [MaxLength(64)]
    public string? DisplayName { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
    public ICollection<RolePermission>? RolePermissions { get; set; }
}
