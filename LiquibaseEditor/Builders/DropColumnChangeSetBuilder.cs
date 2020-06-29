using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.ChangeSetTypes.Elements;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Builders
{
    public class DropColumnChangeSetBuilder
    {
        private readonly Column _column;
        private readonly Table _table;

        public DropColumnChangeSetBuilder(Table table, Column column)
        {
            _table = table;
            _column = column;
        }

        public DropColumnChangeSet Build(ChangeSetCommand command)
        {
            return new DropColumnChangeSet
            {
                Id = command.Id,
                Author = command.Author,
                PreConditions = new DropColumnChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    ColumnExists = new ColumnExistsElement
                    {
                        ColumnName = _column.Name,
                        TableName = _table.Name
                    }
                },
                DropColumn = new DropColumnChangeSet.DropColumnElement
                {
                    ColumnName = _column.Name,
                    TableName = _table.Name
                }
            };
        }
    }
}