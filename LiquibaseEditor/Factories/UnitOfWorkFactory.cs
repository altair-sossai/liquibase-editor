using LiquibaseEditor.Constants;
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
                Databases.SqlServer => new UnitOfWorkSqlServerDatabase(connectionString),
                Databases.Oracle => new UnitOfWorkOracleDatabase(connectionString),
                _ => null
            };
        }
    }
}