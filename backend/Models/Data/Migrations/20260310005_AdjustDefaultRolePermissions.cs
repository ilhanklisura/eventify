namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310005)]
public class AdjustDefaultRolePermissions : Migration
{
    public override void Up()
    {
        // attendee/organizer should NOT have user_list by default (admin role is for user management)
        Execute.Sql(@"
            DELETE rp
            FROM RolePermissions rp
            WHERE rp.RoleId IN (
                SELECT Id FROM Roles WHERE Name IN ('attendee', 'organizer')
            )
              AND rp.PermissionId = (SELECT TOP 1 Id FROM Permissions WHERE Name = 'user_list');
        ");
    }

    public override void Down()
    {
        // restore attendee/organizer -> user_list
        Execute.Sql(@"
            INSERT INTO RolePermissions(RoleId, PermissionId)
            SELECT
              r.Id,
              (SELECT TOP 1 Id FROM Permissions WHERE Name = 'user_list')
            FROM Roles r
            WHERE r.Name IN ('attendee', 'organizer')
              AND NOT EXISTS (
              SELECT 1 FROM RolePermissions rp
              WHERE rp.RoleId = r.Id
                AND rp.PermissionId = (SELECT TOP 1 Id FROM Permissions WHERE Name = 'user_list')
            );
        ");
    }
}

