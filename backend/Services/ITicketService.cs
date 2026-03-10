namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.Ticket;
using Eventify.Backend.Models.Response.Ticket;
using Eventify.Backend.Services.Result;

public interface ITicketService : IService
{
    ServiceResult<TicketModel> GetById(int id);
    ServiceResult<List<TicketModel>> GetAll();
    ServiceResult<TicketModel> Create(CreateTicketRequestModel model);
    ServiceResult<TicketModel> Update(UpdateTicketRequestModel model);
    ServiceResult Delete(int id);
}
