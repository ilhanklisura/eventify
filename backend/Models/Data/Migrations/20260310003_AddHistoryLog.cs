namespace Eventify.Backend.Models.Data.Migrations;

using FluentMigrator;

[Migration(20260310003)]
public class AddHistoryLog : Migration
{
    public override void Up()
    {
        Create.Table("HistoryLog")
            // AutoHistory's key is int in EF; keep this int to avoid InvalidCastException (bigint->int)
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
        Delete.Table("HistoryLog");
    }
}
