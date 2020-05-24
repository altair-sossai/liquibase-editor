using System.Collections.Generic;
using System.Linq;
using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Extensions;

namespace LiquibaseEditor.Builders
{
    public class CreateTableChangeSetBuilder
    {
        private readonly List<Column> _columns;
        private readonly Table _table;

        public CreateTableChangeSetBuilder(Table table, List<Column> columns)
        {
            _table = table;
            _columns = columns;
        }

        public CreateTableChangeSet Build(ChangeSetCommand command)
        {
            var changeSet = new CreateTableChangeSet
            {
                Id = command.Id,
                Author = command.Author,
                PreConditions = new CreateTableChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    Not = new CreateTableChangeSet.NotElement
                    {
                        TableExists = new CreateTableChangeSet.TableExistsElement
                        {
                            TableName = _table.Name
                        }
                    }
                },
                CreateTable = new CreateTableChangeSet.CreateTableElement
                {
                    TableName = _table.Name,
                    Remarks = _table.Name,
                    SchemaName = "${dbSchemaName}",
                    Column = BuildColumns(command)
                }
            };

            return changeSet;
        }

        private List<CreateTableChangeSet.ColumnElement> BuildColumns(ChangeSetCommand command)
        {
            return _columns
                .Select(column => BuildColumn(command, column))
                .ToList();
        }

        private CreateTableChangeSet.ColumnElement BuildColumn(ChangeSetCommand command, Column column)
        {
            return new CreateTableChangeSet.ColumnElement
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