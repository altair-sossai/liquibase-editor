using LiquibaseEditor.Commands;

namespace LiquibaseEditor.Extensions
{
    public static class GenerateCommandExtensions
    {
        public static void UseSqlServerDefaultConfiguration(this GenerateCommand command)
        {
            command.ConnectionString = "Server=localhost;Database=FC_SIMPLEFARM_DEV2004;User Id=sa;Password=ef66b58b-6ff2-4c78-bcec-6b279312b625;";
        }

        public static void UseOracleDefaultConfiguration(this GenerateCommand command)
        {
            command.ConnectionString = "USER ID=GATEC_SIMPLEFARM_QA;PASSWORD=ga;DATA SOURCE=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(HOST = 10.0.1.25)(PORT = 1524)))(CONNECT_DATA =(SERVICE_NAME = DBDEV11)))";
        }
    }
}