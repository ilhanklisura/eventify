namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Request.Booking;
using Eventify.Backend.Models.Response.Booking;
using Eventify.Backend.Services.Result;

public interface IBookingService : IService
{
    ServiceResult<BookingModel> GetById(int id);
    ServiceResult<List<BookingModel>> GetAll();
    ServiceResult<BookingModel> Create(CreateBookingRequestModel model);
    ServiceResult Delete(int id);
}
