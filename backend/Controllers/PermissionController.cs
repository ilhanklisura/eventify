namespace Eventify.Backend.Controllers;

using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ApiController
{
    private readonly IPermissionService _service;

    public PermissionController(IPermissionService service) => _service = service;

    /// <summary>Lista svih permisija u sustavu (za admin UI).</summary>
    [HttpGet]
    [Authorize("claim:permission:user_list")]
    public ActionResult GetAll() => _service.GetAllPermissions().ToActionResult();
}
