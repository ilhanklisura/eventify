namespace Eventify.Backend.Models.Response.Event;

using Eventify.Backend.Models.Response.Category;
using Eventify.Backend.Models.Response.Common;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Models.Response.Venue;

public class EventModel : BaseResponseModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTime Date { get; set; }
    public int CategoryId { get; set; }
    public CategoryModel? Category { get; set; }
    public int VenueId { get; set; }
    public VenueModel? Venue { get; set; }
    public int OrganizerId { get; set; }
    public UserModel? Organizer { get; set; }
}
