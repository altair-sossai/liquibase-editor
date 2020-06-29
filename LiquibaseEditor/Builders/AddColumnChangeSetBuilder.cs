using LiquibaseEditor.Builders.Elements;
using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.ChangeSetTypes.Elements;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Builders
{
    public class AddColumnChangeSetBuilder
    {
        private readonly Column _column;
        private readonly Table _table;

        public AddColumnChangeSetBuilder(Table table, Column column)
        {
            _table = table;
            _column = column;
        }

        public AddColumnChangeSet Build(ChangeSetCommand command)
        {
            var columnBuilder = new ColumnElementBuilder(_table);

            return new AddColumnChangeSet
            {
                Id = command.Id,
                Author = command.Author,
                PreConditions = new AddColumnChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    OnFailMessage = "Campo já existe",
                    Not = new AddColumnChangeSet.NotElement
                    {
                        ColumnExists = new ColumnExistsElement
                        {
                            ColumnName = _column.Name,
                            TableName = _table.Name
                        }
                    }
                },
                AddColumn = new AddColumnChangeSet.AddColumnElement
                {
                    SchemaName = "${dbSchemaName}",
                    TableName = _table.Name,
                    Column = columnBuilder.Build(_column)
                }
            };
        }
    }
}