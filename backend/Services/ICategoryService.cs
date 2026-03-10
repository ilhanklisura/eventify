namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.Category;
using Eventify.Backend.Models.Response.Category;
using Eventify.Backend.Services.Result;

public interface ICategoryService : IService
{
    ServiceResult<CategoryModel> GetById(int id);
    ServiceResult<List<CategoryModel>> GetAll();
    ServiceResult<CategoryModel> Create(CreateCategoryRequestModel model);
    ServiceResult<CategoryModel> Update(UpdateCategoryRequestModel model);
    ServiceResult Delete(int id);
}
