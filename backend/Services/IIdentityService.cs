namespace Eventify.Backend.Services;

using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Services.Result;

/// <summary>Trenutni ulogirani korisnik (iz JWT claims / HttpContext).</summary>
public interface IIdentityService : IService
{
    ServiceResult<string> GetCurrentUserName();
    User? CurrentUser(); // null ako nije ulogiran
}
