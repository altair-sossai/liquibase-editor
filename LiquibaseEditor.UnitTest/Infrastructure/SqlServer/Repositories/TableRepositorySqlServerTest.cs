using LiquibaseEditor.Infrastructure.SqlServer.Repositories;
using LiquibaseEditor.UnitTest.Infrastructure.SqlServer.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LiquibaseEditor.UnitTest.Infrastructure.SqlServer.Repositories
{
    [TestClass]
    public class TableRepositorySqlServerTest
    {
        [TestMethod]
        public void GetAll()
        {
            using var unitOfWork = UnitOfWorkSqlServerDatabaseBuilder.New();

            var tableRepository = new TableRepositorySqlServer(unitOfWork);
            var tables = tableRepository.GetAll();

            Assert.AreNotEqual(0, tables.Count);
        }


        [TestMethod]
        public void GetColumns()
        {
            using var unitOfWork = UnitOfWorkSqlServerDatabaseBuilder.New();

            var tableRepository = new TableRepositorySqlServer(unitOfWork);
            var columns = tableRepository.GetColumns("GA_TIMESHEET");

            Assert.AreNotEqual(0, columns.Count);
        }
    }
}