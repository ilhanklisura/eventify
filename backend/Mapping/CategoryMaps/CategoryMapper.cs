namespace Eventify.Backend.Mapping.CategoryMaps;

using Eventify.Backend.Mapping;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Response.Category;

public class CategoryMapper : IMapper<Category, CategoryModel>
{
    public CategoryModel Map(Category value) => new()
    {
        Id = value.Id,
        Name = value.Name,
        CreatedAt = value.CreatedOn
    };
}
