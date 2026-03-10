namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310001)]
public class InitialSchema : Migration
{
    public override void Up()
    {
        Create.Table("Categories")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithBaseModelColumns();

        Create.Table("Users")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Email").AsString(150).NotNullable()
            .WithColumn("Password").AsString(255).NotNullable()
            .WithColumn("Role").AsString(20).NotNullable()
            .WithBaseModelColumns();

        Create.Table("Venues")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(255).NotNullable()
            .WithColumn("Location").AsString(255).NotNullable()
            .WithBaseModelColumns();

        Create.Table("Events")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Title").AsString(255).NotNullable()
            .WithColumn("Description").AsString().NotNullable()
            .WithColumn("Date").AsDateTime2().NotNullable()
            .WithColumn("CategoryId").AsInt32().NotNullable().ForeignKey("Categories", "Id").OnDelete(System.Data.Rule.None)
            .WithColumn("VenueId").AsInt32().NotNullable().ForeignKey("Venues", "Id").OnDelete(System.Data.Rule.None)
            .WithColumn("OrganizerId").AsInt32().NotNullable().ForeignKey("Users", "Id").OnDelete(System.Data.Rule.None)
            .WithBaseModelColumns();

        Create.Table("Bookings")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("EventId").AsInt32().NotNullable().ForeignKey("Events", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithBaseModelColumns();

        Create.Table("Tickets")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("EventId").AsInt32().NotNullable().ForeignKey("Events", "Id").OnDelete(System.Data.Rule.Cascade)
            .WithColumn("UserId").AsInt32().NotNullable().ForeignKey("Users", "Id").OnDelete(System.Data.Rule.None)
            .WithColumn("Price").AsDecimal(10, 2).NotNullable()
            .WithColumn("Status").AsString(20).NotNullable()
            .WithBaseModelColumns();

        Create.UniqueConstraint("IX_Categories_Name").OnTable("Categories").Column("Name");
        Create.UniqueConstraint("IX_Users_Email").OnTable("Users").Column("Email");
        Create.Index("IX_Events_CategoryId").OnTable("Events").OnColumn("CategoryId");
        Create.Index("IX_Events_VenueId").OnTable("Events").OnColumn("VenueId");
        Create.Index("IX_Events_OrganizerId").OnTable("Events").OnColumn("OrganizerId");
        Create.Index("IX_Bookings_UserId").OnTable("Bookings").OnColumn("UserId");
        Create.Index("IX_Bookings_EventId").OnTable("Bookings").OnColumn("EventId");
        Create.Index("IX_Tickets_EventId").OnTable("Tickets").OnColumn("EventId");
        Create.Index("IX_Tickets_UserId").OnTable("Tickets").OnColumn("UserId");
    }

    public override void Down()
    {
        Delete.Table("Tickets");
        Delete.Table("Bookings");
        Delete.Table("Events");
        Delete.Table("Venues");
        Delete.Table("Users");
        Delete.Table("Categories");
    }
}
