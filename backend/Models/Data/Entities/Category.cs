namespace Eventify.Backend.Models.Data.Entities;

using System.ComponentModel.DataAnnotations;

public class Category : BaseModel
{
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}
