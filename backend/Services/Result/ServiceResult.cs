namespace Eventify.Backend.Services.Result;

using Microsoft.AspNetCore.Mvc;

public class ServiceResult<T>
{
    public ResultKind Kind { get; private set; }
    public T? Value { get; private set; }
    public string? Message { get; private set; }
    public bool IsOk => Kind == ResultKind.Ok;

    protected ServiceResult(ResultKind kind, T? value) { Kind = kind; Value = value; }
    protected ServiceResult(ResultKind kind, string? message) { Kind = kind; Message = message; }

    public static ServiceResult<T> Ok(T value) => new(ResultKind.Ok, value);

    public static implicit operator ServiceResult<T>(ServiceResult result)
    {
        if (typeof(T) == typeof(NoValue))
            return (ServiceResult<T>)(object)result;
        return new ServiceResult<T>(result.Kind, result.Message);
    }

    public ActionResult ToActionResult()
    {
        switch (Kind)
        {
            case ResultKind.Ok: return new OkObjectResult(Value);
            case ResultKind.NotFound: return new NotFoundResult();
            case ResultKind.Error:
            case ResultKind.ValidationError: return new BadRequestObjectResult(new { Message });
            case ResultKind.MissingEntity: return new NotFoundObjectResult(new { Message });
            case ResultKind.ExistingEntity: return new ObjectResult(new { Message }) { StatusCode = 409 };
            default: return new ObjectResult($"Unknown: {Kind}") { StatusCode = 500 };
        }
    }
}

public class ServiceResult : ServiceResult<NoValue>
{
    private ServiceResult(ResultKind kind) : base(kind, (NoValue?)null) { }
    private ServiceResult(ResultKind kind, string msg) : base(kind, (string?)msg) { }

    public static ServiceResult Ok() => new(ResultKind.Ok);
    public static ServiceResult NotFound() => new(ResultKind.NotFound, "Entity not found.");
    public static ServiceResult Error(string msg) => new(ResultKind.Error, msg);
    public static ServiceResult ValidationError(string msg) => new(ResultKind.ValidationError, msg);
    public static ServiceResult MissingEntity(string name) =>
        new(ResultKind.MissingEntity, $"Dependent entity '{name}' not found.");
    public static ServiceResult ExistingEntity(string prop) =>
        new(ResultKind.ExistingEntity, $"Conflict on property '{prop}'.");

    public new ActionResult ToActionResult()
    {
        switch (Kind)
        {
            case ResultKind.Ok: return new OkResult();
            case ResultKind.NotFound: return new NotFoundResult();
            case ResultKind.Error:
            case ResultKind.ValidationError: return new BadRequestObjectResult(new { Message });
            case ResultKind.MissingEntity: return new NotFoundObjectResult(new { Message });
            case ResultKind.ExistingEntity: return new ObjectResult(new { Message }) { StatusCode = 409 };
            default: return new ObjectResult($"Unknown: {Kind}") { StatusCode = 500 };
        }
    }
}
