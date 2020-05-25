using LiquibaseEditor.Infrastructure.Oracle.Repositories;
using LiquibaseEditor.Infrastructure.Oracle.UnitOfWork;
using LiquibaseEditor.Infrastructure.SqlServer.Repositories;
using LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork;
using LiquibaseEditor.Repositories;
using LiquibaseEditor.UnitOfWork;

namespace LiquibaseEditor.Factories
{
    public static class TableRepositoryFactory
    {
        public static ITableRepository New(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Database switch
            {
                "SQL Server" => new TableRepositorySqlServer(unitOfWork as UnitOfWorkSqlServerDatabase),
                "Oracle" => new TableRepositoryOracle(unitOfWork as UnitOfWorkOracleDatabase),
                _ => null
            };
        }
    }
}