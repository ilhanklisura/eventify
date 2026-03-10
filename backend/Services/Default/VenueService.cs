namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Models.Data;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Request.Venue;
using Eventify.Backend.Models.Response.Venue;
using Eventify.Backend.Services.Result;
using Microsoft.EntityFrameworkCore;

public class VenueService : Service, IVenueService
{
    private readonly DataContext _db;

    public VenueService(IServiceProvider sp, DataContext db) : base(sp) => _db = db;

    public ServiceResult<VenueModel> GetById(int id)
    {
        var e = _db.Venues.AsNoTracking().FirstOrDefault(x => x.Id == id);
        return e == null ? NotFound() : Ok(Map(e));
    }

    public ServiceResult<List<VenueModel>> GetAll()
    {
        var list = _db.Venues.AsNoTracking().OrderBy(x => x.Name).Select(Map).ToList();
        return Ok(list);
    }

    public ServiceResult<VenueModel> Create(CreateVenueRequestModel model)
    {
        var e = new Venue { Name = model.Name, Location = model.Location };
        _db.Venues.Add(e);
        _db.SaveChanges();
        return Ok(Map(e));
    }

    public ServiceResult<VenueModel> Update(UpdateVenueRequestModel model)
    {
        var e = _db.Venues.Find(model.Id);
        if (e == null) return NotFound();
        e.Name = model.Name;
        e.Location = model.Location;
        _db.SaveChanges();
        return Ok(Map(e));
    }

    public ServiceResult Delete(int id)
    {
        var e = _db.Venues.Find(id);
        if (e == null) return NotFound();
        _db.Venues.Remove(e);
        _db.SaveChanges();
        return Ok();
    }

    private static VenueModel Map(Venue x) => new() { Id = x.Id, Name = x.Name, Location = x.Location, CreatedAt = x.CreatedOn };
}
