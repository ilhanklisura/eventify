namespace FluentMigrator;

using FluentMigrator.Builders.Create.Table;

public static class FluentMigratorExtensions
{
    public static ICreateTableWithColumnSyntax WithBaseModelColumns(
        this ICreateTableWithColumnSyntax builder)
    {
        return builder
            .WithColumn("CreatedBy").AsString(64).WithDefaultValue("unknown")
            .WithColumn("CreatedOn").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("UpdatedBy").AsString(64).WithDefaultValue("unknown")
            .WithColumn("UpdatedOn").AsDateTime().WithDefault(SystemMethods.CurrentDateTime)
            .WithColumn("IsDeleted").AsBoolean().WithDefaultValue(false).Indexed()
            .WithColumn("DeletedBy").AsString(64).Nullable()
            .WithColumn("DeletedOn").AsDateTime().Nullable();
    }
}
