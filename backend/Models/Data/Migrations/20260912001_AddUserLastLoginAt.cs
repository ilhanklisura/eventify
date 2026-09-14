namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260912001)]
public class AddUserLastLoginAt : Migration
{
    public override void Up()
    {
        Alter.Table("Users")
            .AddColumn("LastLoginAt").AsDateTime2().Nullable();
    }

    public override void Down()
    {
        Delete.Column("LastLoginAt").FromTable("Users");
    }
}
