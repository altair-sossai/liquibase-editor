using System;
using System.Collections.Generic;
using System.Linq;

namespace LiquibaseEditor.Helpers
{
    public static class TableHelper
    {
        public static List<string> ParseTableNames(string tableNames)
        {
            return tableNames
                .Split(";", StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Distinct()
                .ToList();
        }
    }
}