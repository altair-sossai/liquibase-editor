using System;
using System.Collections.Generic;
using LiquibaseEditor.Builders;
using LiquibaseEditor.Commands;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Enums;
using LiquibaseEditor.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiquibaseEditor.UnitTest.Builders
{
    [TestClass]
    public class CreateTableChangeSetBuilderTest
    {
        [TestMethod]
        public void Build()
        {
            var table = new Table
            {
                Name = "GA_TIMESHEET"
            };

            var columns = new List<Column>
            {
                new Column
                {
                    Name = "ID",
                    PrimaryKey = true,
                    ForeignKey = false,
                    Nullable = false,
                    Type = ColumnType.Long,
                    Length = null,
                    Precision = 19,
                    Scale = 0
                },
                new Column
                {
                    Name = "TIMESHEET_ID",
                    PrimaryKey = false,
                    ForeignKey = true,
                    Nullable = false,
                    Type = ColumnType.Long,
                    Length = null,
                    Precision = 19,
                    Scale = 0
                }
            };

            var builder = new CreateTableChangeSetBuilder(table, columns);
            var command = new ChangeSetCommand("altair.sossai");

            var changeSet = builder.Build(command);

            Console.WriteLine(changeSet.ToXml());
        }
    }
}