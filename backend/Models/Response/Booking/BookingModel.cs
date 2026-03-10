namespace Eventify.Backend.Models.Response.Booking;

using Eventify.Backend.Models.Response.Common;
using Eventify.Backend.Models.Response.Event;
using Eventify.Backend.Models.Response.User;

public class BookingModel : BaseResponseModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserModel? User { get; set; }
    public int EventId { get; set; }
    public EventModel? Event { get; set; }
}
