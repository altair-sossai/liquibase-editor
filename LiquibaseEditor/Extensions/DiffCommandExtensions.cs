using LiquibaseEditor.Commands;

namespace LiquibaseEditor.Extensions
{
    public static class DiffCommandExtensions
    {
        public static void UseSqlServerDefaultConfiguration(this DiffCommand command)
        {
            command.UseSourceSqlServerDefaultConfiguration();
            command.UseTargetSqlServerDefaultConfiguration();
        }

        public static void UseOracleDefaultConfiguration(this DiffCommand command)
        {
            command.UseSourceOracleDefaultConfiguration();
            command.UseTargetOracleDefaultConfiguration();
        }

        public static void UseSourceSqlServerDefaultConfiguration(this DiffCommand command)
        {
            command.SourceConnectionString = "Server=localhost;Database=FC_SIMPLEFARM_DEV2004;User Id=sa;Password=ef66b58b-6ff2-4c78-bcec-6b279312b625;";
        }

        public static void UseSourceOracleDefaultConfiguration(this DiffCommand command)
        {
            command.SourceConnectionString = "USER ID=GATEC_SIMPLEFARM_QA;PASSWORD=ga;DATA SOURCE=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.25)(PORT = 1524)))(CONNECT_DATA =(SERVICE_NAME = DBDEV11)))";
        }

        public static void UseTargetSqlServerDefaultConfiguration(this DiffCommand command)
        {
            command.TargetConnectionString = "Server=localhost;Database=FC_SIMPLEFARM_DEV2004;User Id=sa;Password=ef66b58b-6ff2-4c78-bcec-6b279312b625;";
        }

        public static void UseTargetOracleDefaultConfiguration(this DiffCommand command)
        {
            command.TargetConnectionString = "USER ID=GATEC_SIMPLEFARM_QA;PASSWORD=ga;DATA SOURCE=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.25)(PORT = 1524)))(CONNECT_DATA =(SERVICE_NAME = DBDEV11)))";
        }
    }
}