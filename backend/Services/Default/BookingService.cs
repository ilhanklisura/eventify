namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.Booking;
using Eventify.Backend.Models.Response.Booking;
using Eventify.Backend.Models.Response.Event;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class BookingService : Service, IBookingService
{
    private readonly DataContext _db;

    public BookingService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public ServiceResult<BookingModel> GetById(int id)
    {
        var e = _db.Bookings.AsNoTracking().Include(x => x.User).Include(x => x.Event).FirstOrDefault(x => x.Id == id);
        return e == null ? NotFound() : Ok(Map(e));
    }

    public ServiceResult<List<BookingModel>> GetAll()
    {
        var list = _db.Bookings.AsNoTracking().Include(x => x.User).Include(x => x.Event).OrderBy(x => x.Id).Select(Map).ToList();
        return Ok(list);
    }

    public ServiceResult<BookingModel> Create(CreateBookingRequestModel model)
    {
        var e = new Booking { UserId = model.UserId, EventId = model.EventId };
        _db.Bookings.Add(e);
        _db.SaveChanges();
        _db.Entry(e).Reload();
        e = _db.Bookings.Include(x => x.User).Include(x => x.Event).First(x => x.Id == e.Id);
        return Ok(Map(e));
    }

    public ServiceResult Delete(int id)
    {
        var e = _db.Bookings.Find(id);
        if (e == null) return NotFound();
        _db.Bookings.Remove(e);
        _db.SaveChanges();
        return Ok();
    }

    private static BookingModel Map(Booking x) => new()
    {
        Id = x.Id,
        UserId = x.UserId,
        User = x.User == null ? null : new UserModel { Id = x.User.Id, Name = x.User.Name, Email = x.User.Email, Role = x.User.Role, CreatedAt = x.User.CreatedOn },
        EventId = x.EventId,
        Event = x.Event == null ? null : new EventModel { Id = x.Event.Id, Title = x.Event.Title, Description = x.Event.Description, Date = x.Event.Date, CategoryId = x.Event.CategoryId, VenueId = x.Event.VenueId, OrganizerId = x.Event.OrganizerId, CreatedAt = x.Event.CreatedOn },
        CreatedAt = x.CreatedOn
    };
}
