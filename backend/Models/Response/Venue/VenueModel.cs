namespace Eventify.Backend.Models.Response.Venue;

using Eventify.Backend.Models.Response.Common;

public class VenueModel : BaseResponseModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
}
