using System;
using LiquibaseEditor.ChangeSetTypes;
using LiquibaseEditor.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiquibaseEditor.UnitTest.ChangeSetTypes
{
    [TestClass]
    public class CreateTableChangeSetTest
    {
        [TestMethod]
        public void ToXml()
        {
            var changeSet = new CreateTableChangeSet
            {
                Id = "20200408-1-r2",
                Author = "altair.sossai",
                PreConditions = new CreateTableChangeSet.PreConditionsElement
                {
                    OnFail = "MARK_RAN",
                    Not = new CreateTableChangeSet.NotElement
                    {
                        TableExists = new CreateTableChangeSet.TableExistsElement
                        {
                            TableName = "GA_TELEGRAM"
                        }
                    }
                }
            };

            var xml = changeSet.ToXml();

            Console.WriteLine(xml);
        }
    }
}