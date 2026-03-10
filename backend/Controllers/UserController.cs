namespace Eventify.Backend.Controllers;

using Eventify.Backend.Models.Request.User;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UserController : ApiController
{
    private readonly IUserService _service;

    public UserController(IUserService service) => _service = service;

    [HttpGet]
    [Authorize("claim:permission:user_list")]
    public ActionResult GetAll() => _service.GetAll().ToActionResult();

    [HttpGet("{id:int}")]
    [Authorize("claim:permission:user_list")]
    public ActionResult GetById(int id) => _service.GetById(id).ToActionResult();

    [HttpPost]
    [Authorize("claim:permission:user_list")]
    public ActionResult Create([FromBody] CreateUserRequestModel model) => _service.Create(model).ToActionResult();

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] UpdateUserRequestModel model)
    {
        if (model.Id != id) return BadRequest();
        return _service.Update(model).ToActionResult();
    }

    [HttpDelete("{id:int}")]
    [Authorize("claim:permission:user_list")]
    public ActionResult Delete(int id) => _service.Delete(id).ToActionResult();
}
