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
            if (unitOfWork.Database == "SQL Server")
                return new TableRepositorySqlServer(unitOfWork as UnitOfWorkSqlServerDatabase);

            return null;
        }
    }
}