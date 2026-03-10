namespace Eventify.Backend.Models.Request.Booking;

using System.ComponentModel.DataAnnotations;

public class CreateBookingRequestModel
{
    public int UserId { get; set; }
    public int EventId { get; set; }
}
