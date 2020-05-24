using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Builders
{
    public class AddForeignKeyConstraintChangeSetBuilder
    {
        private readonly ForeignKey _foreignKey;

        public AddForeignKeyConstraintChangeSetBuilder(ForeignKey foreignKey)
        {
            _foreignKey = foreignKey;
        }

        public AddForeignKeyConstraintChangeSet Build(ChangeSetCommand command)
        {
            return new AddForeignKeyConstraintChangeSet
            {
                Id = command.Id,
                Author = command.Author,
                PreConditions = new AddForeignKeyConstraintChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    Not = new AddForeignKeyConstraintChangeSet.NotElement
                    {
                        ForeignKeyConstraintExists = new AddForeignKeyConstraintChangeSet.ForeignKeyConstraintExistsElement
                        {
                            ForeignKeyName = _foreignKey.Name
                        }
                    }
                },
                AddForeignKeyConstraint = new AddForeignKeyConstraintChangeSet.AddForeignKeyConstraintElement
                {
                    BaseTableName = _foreignKey.TableName,
                    BaseColumnNames = _foreignKey.ColumnName,
                    ConstraintName = _foreignKey.Name,
                    ReferencedTableName = _foreignKey.ReferencedTableName,
                    ReferencedColumnNames = _foreignKey.ReferencedColumnName,
                    Deferrable = "false",
                    InitiallyDeferred = "false",
                    OnDelete = "NO ACTION",
                    OnUpdate = "NO ACTION"
                }
            };
        }
    }
}