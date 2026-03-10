namespace Eventify.Backend.Common.Attributes;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

/// <summary>Provjera da request model nije null – vraća BadRequest ako je.</summary>
public class RequireModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ActionArguments.Values.Contains(null))
            context.Result = new BadRequestObjectResult("The request is invalid. One of the required parameters is null.");
    }
}
