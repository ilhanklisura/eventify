namespace Eventify.Backend.Controllers;

using Eventify.Backend.Common.Attributes;
using Eventify.Backend.Models.Request.Token;
using Eventify.Backend.Models.Request.User;
using Eventify.Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public TokenController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }

    /// <summary>Login with email and password. Returns JWT and user info.</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Models.Response.Token.TokenModel), 200)]
    public ActionResult Login([FromBody] CreateTokenRequestModel model)
    {
        var check = _userService.CheckPassword(model.Email, model.Password);
        if (!check.IsOk) return check.ToActionResult();
        var token = _tokenService.CreateToken(model.Email);
        return token.ToActionResult();
    }

    /// <summary>Register a new user. Returns JWT and user info.</summary>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Models.Response.Token.TokenModel), 200)]
    public ActionResult Register([FromBody] CreateUserRequestModel model)
    {
        // Security: registration always creates attendee accounts.
        // Admin/organizer roles are assigned explicitly by admins.
        model.Role = "attendee";

        var create = _userService.Create(model);
        if (!create.IsOk) return create.ToActionResult();
        var token = _tokenService.CreateToken(create.Value!.Email);
        return token.ToActionResult();
    }
}
