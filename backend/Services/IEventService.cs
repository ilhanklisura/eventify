namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.Event;
using Eventify.Backend.Models.Response.Event;
using Eventify.Backend.Services.Result;

public interface IEventService : IService
{
    ServiceResult<EventModel> GetById(int id);
    ServiceResult<List<EventModel>> GetAll();
    ServiceResult<EventModel> Create(CreateEventRequestModel model);
    ServiceResult<EventModel> Update(UpdateEventRequestModel model);
    ServiceResult Delete(int id);
}
