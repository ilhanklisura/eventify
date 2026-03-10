namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class RolePermission : BaseModel
{
    public int Id { get; set; }
    public int RoleId { get; set; }
    public int PermissionId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }

    [ForeignKey(nameof(PermissionId))]
    public Permission? Permission { get; set; }
}
