namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310002)]
public class AddRolesAndPermissions : Migration
{
    public override void Up()
    {
        Create.Table("Permissions")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(64).NotNullable()
            .WithColumn("Group").AsInt32().NotNullable()
            .WithColumn("DisplayName").AsString(64).Nullable()
            .WithColumn("Description").AsString(256).Nullable()
            .WithBaseModelColumns();

        Create.Table("Roles")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(64).NotNullable()
            .WithColumn("DisplayName").AsString(64).Nullable()
            .WithBaseModelColumns();

        Create.Table("RolePermissions")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("RoleId").AsInt32().NotNullable().ForeignKey("Roles", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("PermissionId").AsInt32().NotNullable().ForeignKey("Permissions", "Id").OnDelete(System.Data.Rule.None)
            .WithBaseModelColumns();

        Create.Table("UserRoles")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("RoleId").AsInt32().NotNullable().ForeignKey("Roles", "Id").OnDelete(System.Data.Rule.None)
            .WithBaseModelColumns();

        Create.UniqueConstraint("IX_Roles_Name").OnTable("Roles").Column("Name");
        Create.Index("IX_RolePermissions_RoleId").OnTable("RolePermissions").OnColumn("RoleId");
        Create.Index("IX_RolePermissions_PermissionId").OnTable("RolePermissions").OnColumn("PermissionId");
        Create.Index("IX_UserRoles_UserId").OnTable("UserRoles").OnColumn("UserId");
        Create.Index("IX_UserRoles_RoleId").OnTable("UserRoles").OnColumn("RoleId");

        // Seed: Roles, Permissions, RolePermissions (defaults za CreatedBy, CreatedOn, UpdatedBy, UpdatedOn, IsDeleted)
        Execute.Sql(@"
            SET IDENTITY_INSERT [Roles] ON;
            INSERT INTO [Roles] (Id, Name, DisplayName) VALUES (1, 'attendee', 'Attendee'), (2, 'organizer', 'Organizer');
            SET IDENTITY_INSERT [Roles] OFF;

            INSERT INTO [Permissions] (Name, [Group], DisplayName) VALUES
            ('event_list', 0, 'List events'), ('event_create', 0, 'Create event'), ('event_edit', 0, 'Edit event'), ('event_delete', 0, 'Delete event'),
            ('booking_list', 0, 'List bookings'), ('user_list', 0, 'List users'), ('codebook_list', 0, 'List codebooks');

            INSERT INTO [RolePermissions] (RoleId, PermissionId)
            SELECT 1, Id FROM [Permissions] WHERE Name IN ('event_list', 'booking_list', 'user_list', 'codebook_list');
            INSERT INTO [RolePermissions] (RoleId, PermissionId)
            SELECT 2, Id FROM [Permissions];
        ");
    }

    public override void Down()
    {
        Delete.Table("RolePermissions");
        Delete.Table("UserRoles");
        Delete.Table("Permissions");
        Delete.Table("Roles");
    }
}
