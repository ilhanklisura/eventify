namespace Eventify.Backend.Models.Data.Util;

using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

public class AuditInterceptor : SaveChangesInterceptor
{
    private readonly IIdentityService _identityService;

    public AuditInterceptor(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        if (eventData.Context == null)
            return result;

        var user = "Unknown";
        var currentUser = _identityService.GetCurrentUserName();
        if (currentUser.IsOk)
            user = currentUser.Value!;

        eventData.Context!.EnsureAutoHistory(() => new HistoryLog { Username = user });

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry.Entity is not BaseModel model)
                continue;

            switch (entry.State)
            {
                case EntityState.Added:
                    model.CreatedOn = DateTime.UtcNow;
                    model.UpdatedOn = DateTime.UtcNow;
                    model.CreatedBy = user;
                    model.UpdatedBy = user;
                    break;

                case EntityState.Modified:
                    model.UpdatedOn = DateTime.UtcNow;
                    model.UpdatedBy = user;
                    break;

                case EntityState.Deleted:
                    model.IsDeleted = true;
                    model.DeletedOn = DateTime.UtcNow;
                    model.DeletedBy = user;
                    entry.State = EntityState.Modified;
                    break;
            }
        }

        return result;
    }
}
