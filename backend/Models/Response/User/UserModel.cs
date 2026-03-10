namespace Eventify.Backend.Models.Response.User;

using Eventify.Backend.Models.Response.Common;

public class UserModel : BaseResponseModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
