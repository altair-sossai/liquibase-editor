using System.Collections.Generic;
using LiquibaseEditor.Entities;

namespace LiquibaseEditor.Repositories
{
    public interface ITableRepository
    {
        List<Table> GetAll();
        List<Column> GetColumns(string table);
        List<ForeignKey> GetForeignKeys(string table);
    }
}