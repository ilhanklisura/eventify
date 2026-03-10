namespace Eventify.Backend.Services;

using System.Security.Claims;
using Eventify.Backend.Models.Response.Token;
using Eventify.Backend.Services.Result;

public interface ITokenService : IService
{
    ServiceResult<TokenModel> CreateToken(string email);
    ServiceResult<ClaimsIdentity> ValidateToken(string token);
}
