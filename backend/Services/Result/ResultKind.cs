namespace Eventify.Backend.Services.Result;

public enum ResultKind
{
    Ok,
    NotFound,
    Error,
    ValidationError,
    MissingEntity,
    ExistingEntity
}
