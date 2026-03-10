namespace Eventify.Backend.Controllers;

using Eventify.Backend.Models.Request.Category;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ApiController
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service) => _service = service;

    [HttpGet]
    public ActionResult GetAll() => _service.GetAll().ToActionResult();

    [HttpGet("{id:int}")]
    public ActionResult GetById(int id) => _service.GetById(id).ToActionResult();

    [HttpPost]
    public ActionResult Create([FromBody] CreateCategoryRequestModel model) => _service.Create(model).ToActionResult();

    [HttpPut("{id:int}")]
    public ActionResult Update(int id, [FromBody] UpdateCategoryRequestModel model)
    {
        if (model.Id != id) return BadRequest();
        return _service.Update(model).ToActionResult();
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id) => _service.Delete(id).ToActionResult();
}
