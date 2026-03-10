namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.Event;
using Eventify.Backend.Models.Response.Category;
using Eventify.Backend.Models.Response.Event;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Models.Response.Venue;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class EventService : Service, IEventService
{
    private readonly DataContext _db;

    public EventService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public ServiceResult<EventModel> GetById(int id)
    {
        var e = _db.Events
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Venue)
            .Include(x => x.Organizer)
            .FirstOrDefault(x => x.Id == id);
        return e == null ? NotFound() : Ok(Map(e));
    }

    public ServiceResult<List<EventModel>> GetAll()
    {
        var list = _db.Events
            .AsNoTracking()
            .Include(x => x.Category)
            .Include(x => x.Venue)
            .Include(x => x.Organizer)
            .OrderBy(x => x.Date)
            .Select(Map)
            .ToList();
        return Ok(list);
    }

    public ServiceResult<EventModel> Create(CreateEventRequestModel model)
    {
        var e = new Event
        {
            Title = model.Title,
            Description = model.Description,
            Date = model.Date,
            CategoryId = model.CategoryId,
            VenueId = model.VenueId,
            OrganizerId = model.OrganizerId
        };
        _db.Events.Add(e);
        _db.SaveChanges();
        _db.Entry(e).Reload();
        e = _db.Events.Include(x => x.Category).Include(x => x.Venue).Include(x => x.Organizer).First(x => x.Id == e.Id);
        return Ok(Map(e));
    }

    public ServiceResult<EventModel> Update(UpdateEventRequestModel model)
    {
        var e = _db.Events.Find(model.Id);
        if (e == null) return NotFound();
        e.Title = model.Title;
        e.Description = model.Description;
        e.Date = model.Date;
        e.CategoryId = model.CategoryId;
        e.VenueId = model.VenueId;
        _db.SaveChanges();
        _db.Entry(e).Reload();
        e = _db.Events.Include(x => x.Category).Include(x => x.Venue).Include(x => x.Organizer).First(x => x.Id == e.Id);
        return Ok(Map(e));
    }

    public ServiceResult Delete(int id)
    {
        var e = _db.Events.Find(id);
        if (e == null) return NotFound();
        _db.Events.Remove(e);
        _db.SaveChanges();
        return Ok();
    }

    private static EventModel Map(Event x) => new()
    {
        Id = x.Id,
        Title = x.Title,
        Description = x.Description,
        Date = x.Date,
        CategoryId = x.CategoryId,
        Category = x.Category == null ? null : new CategoryModel { Id = x.Category.Id, Name = x.Category.Name, CreatedAt = x.Category.CreatedOn },
        VenueId = x.VenueId,
        Venue = x.Venue == null ? null : new VenueModel { Id = x.Venue.Id, Name = x.Venue.Name, Location = x.Venue.Location, CreatedAt = x.Venue.CreatedOn },
        OrganizerId = x.OrganizerId,
        Organizer = x.Organizer == null ? null : new UserModel { Id = x.Organizer.Id, Name = x.Organizer.Name, Email = x.Organizer.Email, Role = x.Organizer.Role, CreatedAt = x.Organizer.CreatedOn },
        CreatedAt = x.CreatedOn
    };
}
