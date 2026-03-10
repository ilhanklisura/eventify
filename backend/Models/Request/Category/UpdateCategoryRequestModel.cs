namespace Eventify.Backend.Models.Request.Category;

using System.ComponentModel.DataAnnotations;

public class UpdateCategoryRequestModel
{
    public int Id { get; set; }
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}
