using System.Collections.Generic;
using LiquibaseEditor.Builders.Elements;
using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;

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
            var columnBuilder = new ColumnElementBuilder(_table);

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
                    Column = columnBuilder.Build(_columns)
                }
            };

            return changeSet;
        }
    }
}