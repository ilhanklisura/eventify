namespace Eventify.Backend.Services;

using Eventify.Backend.Constants;
using Eventify.Backend.Models.Response.Codebook;

/// <summary>Generički dohvat šifarnika (referentnih tablica) za dropdown-e i filtere.</summary>
public interface ICodebookService : IService
{
    CodebookList GetAll(ECodebook codebook);
}
