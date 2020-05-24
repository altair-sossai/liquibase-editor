using LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork;

namespace LiquibaseEditor.UnitTest.Infrastructure.SqlServer.Builders
{
    public static class UnitOfWorkSqlServerDatabaseBuilder
    {
        public static UnitOfWorkSqlServerDatabase New()
        {
            const string connectionString = "Server=localhost;Database=FC_SIMPLEFARM_DEV2004;User Id=sa;Password=ef66b58b-6ff2-4c78-bcec-6b279312b625;";

            return new UnitOfWorkSqlServerDatabase(connectionString);
        }
    }
}