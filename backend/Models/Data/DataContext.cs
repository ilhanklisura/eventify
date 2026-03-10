namespace Eventify.Backend.Models.Data;

using System.Linq.Expressions;
using Eventify.Backend.Models.Data.Entities;
using Eventify.Backend.Models.Data.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class DataContext : DbContext
{
    private readonly IServiceProvider? _serviceProvider;

    public DataContext(DbContextOptions<DataContext> options, IServiceProvider? serviceProvider = null)
        : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_serviceProvider != null)
        {
            var auditInterceptor = ActivatorUtilities.CreateInstance<AuditInterceptor>(_serviceProvider);
            optionsBuilder.AddInterceptors(auditInterceptor);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.EnableAutoHistory<HistoryLog>(o => o.LimitChangedLength = false);
        builder.Entity<HistoryLog>().ToTable("HistoryLog");

        var softDeleteEntities = builder.Model.GetEntityTypes()
            .Select(t => t.ClrType)
            .Where(t => t.IsSubclassOf(typeof(BaseModel)))
            .ToList();

        foreach (var type in softDeleteEntities)
            builder.Entity(type).HasQueryFilter(GetSoftDeleteFilter(type));

        builder.Entity<User>(e => e.HasIndex(u => u.Email).IsUnique());
        builder.Entity<Role>(e => e.HasIndex(r => r.Name).IsUnique());
        builder.Entity<UserRole>(e =>
        {
            e.HasOne(ur => ur.User).WithMany(u => u.UserRoles).HasForeignKey(ur => ur.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(ur => ur.Role).WithMany(r => r.UserRoles).HasForeignKey(ur => ur.RoleId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<RolePermission>(e =>
        {
            e.HasOne(rp => rp.Role).WithMany(r => r.RolePermissions).HasForeignKey(rp => rp.RoleId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(rp => rp.Permission).WithMany(p => p.RolePermissions).HasForeignKey(rp => rp.PermissionId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<Category>(e => e.HasIndex(c => c.Name).IsUnique());
        builder.Entity<Event>(e =>
        {
            e.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Venue).WithMany().HasForeignKey(x => x.VenueId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Organizer).WithMany().HasForeignKey(x => x.OrganizerId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<Ticket>(e =>
        {
            e.HasOne(x => x.Event).WithMany().HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        });
        builder.Entity<Booking>(e =>
        {
            e.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Event).WithMany().HasForeignKey(x => x.EventId).OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static LambdaExpression GetSoftDeleteFilter(Type type)
    {
        var parameter = Expression.Parameter(type, "model");
        var compareValue = Expression.Constant(false);
        var propertyAccess = Expression.PropertyOrField(parameter, nameof(BaseModel.IsDeleted));
        var equalExpression = Expression.Equal(propertyAccess, compareValue);
        return Expression.Lambda(equalExpression, parameter);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Permission> Permissions => Set<Permission>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Venue> Venues => Set<Venue>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<Booking> Bookings => Set<Booking>();
}
