namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310007)]
public class RecreateHistoryLogIntId : Migration
{
    public override void Up()
    {
        // Existing DB already has HistoryLog with BIGINT Id (from earlier migration).
        // AutoHistory expects int key in EF; easiest fix is to drop and recreate the table
        // (it's an audit table, no FKs point to it).

        if (Schema.Table("HistoryLog").Exists())
        {
            Delete.Table("HistoryLog");
        }

        Create.Table("HistoryLog")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("RowId").AsString(50).NotNullable()
            .WithColumn("TableName").AsString(128).NotNullable()
            .WithColumn("Changed").AsString(int.MaxValue).Nullable()
            .WithColumn("Kind").AsInt32().NotNullable()
            .WithColumn("Created").AsDateTime().NotNullable()
            .WithColumn("Username").AsString(64).NotNullable();
    }

    public override void Down()
    {
        // Keep Down simple: drop the table.
        if (Schema.Table("HistoryLog").Exists())
        {
            Delete.Table("HistoryLog");
        }
    }
}

