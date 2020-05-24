using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Builders
{
    public class CreateSequenceChangeSetBuilder
    {
        private readonly Table _table;

        public CreateSequenceChangeSetBuilder(Table table)
        {
            _table = table;
        }

        public CreateSequenceChangeSet Build(ChangeSetCommand command)
        {
            return new CreateSequenceChangeSet
            {
                Id = command.Id,
                Author = command.Author,
                Dbms = "oracle",
                PreConditions = new CreateSequenceChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    SqlCheck = new CreateSequenceChangeSet.SqlCheckElement
                    {
                        ExpectedResult = "0",
                        Text = $"SELECT COUNT(*) FROM USER_SEQUENCES U WHERE UPPER(U.SEQUENCE_NAME) = UPPER('SEQ_{_table.Name}')"
                    }
                },
                CreateSequence = new CreateSequenceChangeSet.CreateSequenceElement
                {
                    Cycle = "true",
                    IncrementBy = "1",
                    MaxValue = "9999999999999",
                    MinValue = "1",
                    Ordered = "true",
                    SchemaName = "${dbSchemaName}",
                    SequenceName = $"SEQ_{_table.Name}",
                    StartValue = "1"
                }
            };
        }
    }
}