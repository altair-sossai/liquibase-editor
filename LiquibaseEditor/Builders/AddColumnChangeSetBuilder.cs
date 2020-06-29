using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Extensions;

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
                        ColumnExists = new AddColumnChangeSet.ColumnExistsElement
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
                    Column = new AddColumnChangeSet.ColumnElement
                    {
                        Name = _column.Name,
                        Type = _column.ColumnType()
                    }
                }
            };
        }
    }
}