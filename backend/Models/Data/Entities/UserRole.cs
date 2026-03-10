namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations.Schema;

public class UserRole : BaseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RoleId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role? Role { get; set; }
}
