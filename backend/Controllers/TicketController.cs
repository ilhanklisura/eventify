namespace Eventify.Backend.Controllers;

using Eventify.Backend.Models.Request.Ticket;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TicketController : ApiController
{
    private readonly ITicketService _service;

    public TicketController(ITicketService service) => _service = service;

    [HttpGet]
    public ActionResult GetAll() => _service.GetAll().ToActionResult();

    [HttpGet("{id:int}")]
    public ActionResult GetById(int id) => _service.GetById(id).ToActionResult();

    [HttpPost]
    public ActionResult Create([FromBody] CreateTicketRequestModel model) => _service.Create(model).ToActionResult();

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] UpdateTicketRequestModel model)
    {
        if (model.Id != id) return BadRequest();
        return _service.Update(model).ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) => _service.Delete(id).ToActionResult();
}
