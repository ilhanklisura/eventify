namespace Eventify.Backend.Models.Request.Category;

using System.ComponentModel.DataAnnotations;

public class CreateCategoryRequestModel
{
    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;
}
