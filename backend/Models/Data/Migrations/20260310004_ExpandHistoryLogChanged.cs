namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310004)]
public class ExpandHistoryLogChanged : Migration
{
    public override void Up()
    {
        Alter.Table("HistoryLog")
            .AlterColumn("Changed").AsString(int.MaxValue).Nullable();
    }

    public override void Down()
    {
        Alter.Table("HistoryLog")
            .AlterColumn("Changed").AsString(255).Nullable();
    }
}

