namespace Eventify.Backend.Controllers.Codebook;

using Eventify.Backend.Constants;
using Eventify.Backend.Controllers;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CodebookController : ApiController
{
    private readonly ICodebookService _codebookService;

    public CodebookController(ICodebookService codebookService) => _codebookService = codebookService;

    /// <summary>Dohvat šifarnika po tipu (Category, EventType, TicketType). Za dropdown-e i filtere.</summary>
    [HttpGet]
    [Authorize("claim:permission:codebook_list")]
    [ProducesResponseType(typeof(Models.Response.Codebook.CodebookList), StatusCodes.Status200OK)]
    public ActionResult Get([FromQuery] ECodebook codebook)
    {
        var result = _codebookService.GetAll(codebook);
        return Ok(result);
    }
}
