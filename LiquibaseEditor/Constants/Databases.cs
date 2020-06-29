using System.Collections.Generic;

namespace LiquibaseEditor.Constants
{
    public static class Databases
    {
        public const string SqlServer = "SQL Server";
        public const string Oracle = "Oracle";

        public static readonly List<string> Types = new List<string>
        {
            SqlServer,
            Oracle
        };
    }
}