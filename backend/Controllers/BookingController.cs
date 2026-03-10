namespace Eventify.Backend.Controllers;

using Eventify.Backend.Models.Request.Booking;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookingController : ApiController
{
    private readonly IBookingService _service;

    public BookingController(IBookingService service) => _service = service;

    [HttpGet]
    public ActionResult GetAll() => _service.GetAll().ToActionResult();

    [HttpGet("{id:int}")]
    public ActionResult GetById(int id) => _service.GetById(id).ToActionResult();

    [HttpPost]
    public ActionResult Create([FromBody] CreateBookingRequestModel model) => _service.Create(model).ToActionResult();

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) => _service.Delete(id).ToActionResult();
}
