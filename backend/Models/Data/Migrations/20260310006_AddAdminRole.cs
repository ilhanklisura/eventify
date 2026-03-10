namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310006)]
public class AddAdminRole : Migration
{
    public override void Up()
    {
        // Create admin role if missing
        Execute.Sql(@"
            IF NOT EXISTS (SELECT 1 FROM Roles WHERE Name = 'admin')
            BEGIN
                INSERT INTO Roles(Name, DisplayName) VALUES ('admin', 'Administrator');
            END
        ");

        // Admin gets all permissions
        Execute.Sql(@"
            INSERT INTO RolePermissions(RoleId, PermissionId)
            SELECT r.Id, p.Id
            FROM Roles r
            CROSS JOIN Permissions p
            WHERE r.Name = 'admin'
              AND NOT EXISTS (
                SELECT 1 FROM RolePermissions rp
                WHERE rp.RoleId = r.Id AND rp.PermissionId = p.Id
              );
        ");
    }

    public override void Down()
    {
        // Remove role permissions and role (keep users intact)
        Execute.Sql(@"
            DELETE rp
            FROM RolePermissions rp
            WHERE rp.RoleId = (SELECT TOP 1 Id FROM Roles WHERE Name = 'admin');
        ");

        Execute.Sql(@"DELETE FROM Roles WHERE Name = 'admin';");
    }
}

