namespace Eventify.Backend.Controllers;

using Eventify.Backend.Models.Request.Event;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EventController : ApiController
{
    private readonly IEventService _service;

    public EventController(IEventService service) => _service = service;

    [HttpGet]
    [Authorize("claim:permission:event_list")]
    public ActionResult GetAll() => _service.GetAll().ToActionResult();

    [HttpGet("{id:int}")]
    [Authorize("claim:permission:event_list")]
    public ActionResult GetById(int id) => _service.GetById(id).ToActionResult();

    [HttpPost]
    [Authorize("claim:permission:event_create")]
    public ActionResult Create([FromBody] CreateEventRequestModel model) => _service.Create(model).ToActionResult();

    [HttpPut("{id:int}")]
    [Authorize("claim:permission:event_edit")]
    public ActionResult Update(int id, [FromBody] UpdateEventRequestModel model)
    {
        if (model.Id != id) return BadRequest();
        return _service.Update(model).ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [Authorize("claim:permission:event_delete")]
    public ActionResult Delete(int id) => _service.Delete(id).ToActionResult();
}
