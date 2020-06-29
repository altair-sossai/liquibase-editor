using System.ComponentModel;
using LiquibaseEditor.ChangeSetTypes.Elements;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Extensions
{
    public static class ColumnExtensions
    {
        public static string ColumnType(this Column column)
        {
            if (column.PrimaryKey || column.ForeignKey)
                return "${id_type}";

            return column.Type switch
            {
                Enums.ColumnType.Bit => "${boolean_type}",
                Enums.ColumnType.Int => "int",
                Enums.ColumnType.Long => "bigint",
                Enums.ColumnType.Number => $"NUMBER({column.Precision ?? 18}, {column.Scale ?? 0})",
                Enums.ColumnType.Char => "char",
                Enums.ColumnType.Varchar => $"VARCHAR({column.Length ?? 200} BYTE)",
                Enums.ColumnType.Guid => "uuid",
                Enums.ColumnType.DateTime => "${datetime_type}",
                _ => throw new InvalidEnumArgumentException()
            };
        }

        public static ConstraintsElement ConstraintsElement(this Column column, Table table)
        {
            return new ConstraintsElement
            {
                Nullable = column.Nullable ? "true" : "false",
                PrimaryKey = column.PrimaryKey ? "true" : null,
                PrimaryKeyName = column.PrimaryKey ? $"PK_{table.Name}" : null
            };
        }
    }
}