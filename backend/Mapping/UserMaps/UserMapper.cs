namespace Eventify.Backend.Mapping.UserMaps;

using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services;

public class UserMapper : IMapper<User, UserModel>
{
    public UserModel Map(User value) => new()
    {
        Id = value.Id,
        Name = value.Name,
        Email = value.Email,
        Role = value.Role,
        CreatedAt = value.CreatedOn
    };
}
