namespace Eventify.Backend.Services.Default;

using Eventify.Backend.Security;
using Eventify.Backend.Services.Result;

public abstract class Service : IService, ISecurityHandler
{
    protected IServiceProvider ServiceProvider { get; }

    protected Service(IServiceProvider serviceProvider) => ServiceProvider = serviceProvider;

    public virtual bool HasRight(string action) => true;

    protected static ServiceResult Ok() => ServiceResult.Ok();
    protected static ServiceResult<T> Ok<T>(T value) => ServiceResult<T>.Ok(value);
    protected static ServiceResult NotFound() => ServiceResult.NotFound();
    protected static ServiceResult Error(string msg) => ServiceResult.Error(msg);
    protected static ServiceResult ValidationError(string msg) => ServiceResult.ValidationError(msg);
    protected static ServiceResult MissingEntity(string name) => ServiceResult.MissingEntity(name);
    protected static ServiceResult ExistingEntity(string prop) => ServiceResult.ExistingEntity(prop);
}
