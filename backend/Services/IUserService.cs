namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.User;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services.Result;

public interface IUserService : IService
{
    ServiceResult<UserModel> GetByEmail(string email);
    ServiceResult<UserModel> GetById(int id);
    ServiceResult<List<UserModel>> GetAll();
    ServiceResult<UserModel> Create(CreateUserRequestModel model);
    ServiceResult<UserModel> Update(UpdateUserRequestModel model);
    ServiceResult Delete(int id);
    ServiceResult CheckPassword(string email, string password);
    ServiceResult<bool> RecordLogin(string email);
}
