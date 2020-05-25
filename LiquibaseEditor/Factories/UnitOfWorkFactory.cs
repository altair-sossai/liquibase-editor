using LiquibaseEditor.Infrastructure.Oracle.UnitOfWork;
using LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork;
using LiquibaseEditor.UnitOfWork;

namespace LiquibaseEditor.Factories
{
    public static class UnitOfWorkFactory
    {
        public static IUnitOfWork New(string type, string connectionString)
        {
            return type switch
            {
                "SQL Server" => new UnitOfWorkSqlServerDatabase(connectionString),
                "Oracle" => new UnitOfWorkOracleDatabase(connectionString),
                _ => null
            };
        }
    }
}