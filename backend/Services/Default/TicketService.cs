namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.Ticket;
using Eventify.Backend.Models.Response.Event;
using Eventify.Backend.Models.Response.Ticket;
using Eventify.Backend.Models.Response.User;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class TicketService : Service, ITicketService
{
    private readonly DataContext _db;

    public TicketService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public ServiceResult<TicketModel> GetById(int id)
    {
        var e = _db.Tickets.AsNoTracking().Include(x => x.Event).Include(x => x.User).FirstOrDefault(x => x.Id == id);
        return e == null ? NotFound() : Ok(Map(e));
    }

    public ServiceResult<List<TicketModel>> GetAll()
    {
        var list = _db.Tickets.AsNoTracking().Include(x => x.Event).Include(x => x.User).OrderBy(x => x.Id).Select(Map).ToList();
        return Ok(list);
    }

    public ServiceResult<TicketModel> Create(CreateTicketRequestModel model)
    {
        var e = new Ticket { EventId = model.EventId, UserId = model.UserId, Price = model.Price, Status = model.Status ?? "Available" };
        _db.Tickets.Add(e);
        _db.SaveChanges();
        _db.Entry(e).Reload();
        e = _db.Tickets.Include(x => x.Event).Include(x => x.User).First(x => x.Id == e.Id);
        return Ok(Map(e));
    }

    public ServiceResult<TicketModel> Update(UpdateTicketRequestModel model)
    {
        var e = _db.Tickets.Find(model.Id);
        if (e == null) return NotFound();
        e.Status = model.Status ?? e.Status;
        _db.SaveChanges();
        return GetById(e.Id);
    }

    public ServiceResult Delete(int id)
    {
        var e = _db.Tickets.Find(id);
        if (e == null) return NotFound();
        _db.Tickets.Remove(e);
        _db.SaveChanges();
        return Ok();
    }

    private static TicketModel Map(Ticket x) => new()
    {
        Id = x.Id,
        EventId = x.EventId,
        Event = x.Event == null ? null : new EventModel { Id = x.Event.Id, Title = x.Event.Title, Description = x.Event.Description, Date = x.Event.Date, CategoryId = x.Event.CategoryId, VenueId = x.Event.VenueId, OrganizerId = x.Event.OrganizerId, CreatedAt = x.Event.CreatedOn },
        UserId = x.UserId,
        User = x.User == null ? null : new UserModel { Id = x.User.Id, Name = x.User.Name, Email = x.User.Email, Role = x.User.Role, CreatedAt = x.User.CreatedOn },
        Price = x.Price,
        Status = x.Status,
        CreatedAt = x.CreatedOn
    };
}
