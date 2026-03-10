namespace Eventify.Backend.Models.Response.Category;

using Eventify.Backend.Models.Response.Common;

public class CategoryModel : BaseResponseModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
}
