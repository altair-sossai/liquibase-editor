using System.Collections.Generic;
using System.Linq;
using LiquibaseEditor.ChangeSetTypes.Elements;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Extensions;

namespace LiquibaseEditor.Builders.Elements
{
    public class ColumnElementBuilder
    {
        private readonly Table _table;

        public ColumnElementBuilder(Table table)
        {
            _table = table;
        }

        public List<ColumnElement> Build(IEnumerable<Column> columns)
        {
            return columns
                .Select(Build)
                .ToList();
        }

        public ColumnElement Build(Column column)
        {
            return new ColumnElement
            {
                Name = column.Name,
                Type = column.ColumnType(),
                Remarks = column.Name,
                AutoIncrement = column.AutoIncrement ? "${autoIncrement}" : null,
                Constraints = column.ConstraintsElement(_table)
            };
        }
    }
}