namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.Venue;
using Eventify.Backend.Models.Response.Venue;
using Eventify.Backend.Services.Result;

public interface IVenueService : IService
{
    ServiceResult<VenueModel> GetById(int id);
    ServiceResult<List<VenueModel>> GetAll();
    ServiceResult<VenueModel> Create(CreateVenueRequestModel model);
    ServiceResult<VenueModel> Update(UpdateVenueRequestModel model);
    ServiceResult Delete(int id);
}
