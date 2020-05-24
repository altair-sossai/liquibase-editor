using LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork;
using LiquibaseEditor.UnitOfWork;

namespace LiquibaseEditor.Factories
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork New(string type, string connectionString)
        {
            if (type == "SQL Server")
                return new UnitOfWorkSqlServerDatabase(connectionString);

            return null;
        }
    }
}