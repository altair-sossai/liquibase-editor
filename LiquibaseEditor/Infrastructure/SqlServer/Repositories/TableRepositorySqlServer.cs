using System.Collections.Generic;
using System.Linq;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Infrastructure.SqlServer.Models;
using LiquibaseEditor.Infrastructure.SqlServer.UnitOfWork;
using LiquibaseEditor.Repositories;

namespace LiquibaseEditor.Infrastructure.SqlServer.Repositories
{
    public class TableRepositorySqlServer : ITableRepository
    {
        private readonly UnitOfWorkSqlServerDatabase _unitOfWork;

        public TableRepositorySqlServer(UnitOfWorkSqlServerDatabase unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Table> GetAll()
        {
            const string query = @"SELECT TABLE_NAME as Name 
                                     FROM INFORMATION_SCHEMA.TABLES 
                                    WHERE TABLE_TYPE = 'BASE TABLE' 
                                 ORDER BY TABLE_NAME";

            return _unitOfWork.Query<Table>(query);
        }

        public List<Column> GetColumns(string table)
        {
            const string query = @"SELECT DISTINCT
	                                      c.COLUMN_NAME as Name,
										  sc.IS_IDENTITY as AutoIncrement,
	                                      CASE WHEN tcpk.CONSTRAINT_TYPE = 'PRIMARY KEY' THEN 1 ELSE 0 END AS PrimaryKey,
	                                      CASE WHEN tcfk.CONSTRAINT_TYPE = 'FOREIGN KEY' THEN 1 ELSE 0 END AS ForeignKey,
	                                      CASE WHEN c.IS_NULLABLE = 'NO' THEN 0 ELSE 1 END AS Nullable,
	                                      c.DATA_TYPE AS DataType,
	                                      c.CHARACTER_MAXIMUM_LENGTH AS Length,
	                                      c.NUMERIC_PRECISION AS Precision,
	                                      c.NUMERIC_SCALE AS Scale,
	                                      c.ORDINAL_POSITION
                                     FROM INFORMATION_SCHEMA.COLUMNS c
							   INNER JOIN SYS.COLUMNS sc ON OBJECT_ID = OBJECT_ID(@table)
							                            AND sc.NAME = c.COLUMN_NAME
                                LEFT JOIN INFORMATION_SCHEMA.KEY_COLUMN_USAGE kcu ON kcu.TABLE_CATALOG = c.TABLE_CATALOG
                                                                                 AND kcu.TABLE_SCHEMA = c.TABLE_SCHEMA
                                                                                 AND kcu.TABLE_NAME = c.TABLE_NAME
                                                                                 AND kcu.COLUMN_NAME = c.COLUMN_NAME
                                LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tcpk ON tcpk.CONSTRAINT_TYPE = 'PRIMARY KEY'
                                                                                   AND tcpk.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                                                                                   AND tcpk.TABLE_SCHEMA = kcu.TABLE_SCHEMA
                                                                                   AND tcpk.TABLE_NAME = kcu.TABLE_NAME
                                LEFT JOIN INFORMATION_SCHEMA.TABLE_CONSTRAINTS tcfk ON tcfk.CONSTRAINT_TYPE = 'FOREIGN KEY'
                                                                                      AND tcfk.CONSTRAINT_NAME = kcu.CONSTRAINT_NAME
                                                                                      AND tcfk.TABLE_SCHEMA = kcu.TABLE_SCHEMA
                                                                                      AND tcfk.TABLE_NAME = kcu.TABLE_NAME
                                    WHERE c.TABLE_NAME = @table
                                 ORDER BY c.ORDINAL_POSITION";

            return _unitOfWork.Query<ColumnAdapter>(query, new {table}).Cast<Column>().ToList();
        }

        public List<ForeignKey> GetForeignKeys(string table)
        {
            const string query = @" SELECT OBJ.NAME AS [Name],
                                           TAB1.NAME AS [TableName],
                                           COL1.NAME AS [ColumnName],
                                           TAB2.NAME AS [ReferencedTableName],
                                           COL2.NAME AS [ReferencedColumnName]
                                      FROM SYS.FOREIGN_KEY_COLUMNS FKC
                                INNER JOIN SYS.OBJECTS OBJ ON OBJ.OBJECT_ID = FKC.CONSTRAINT_OBJECT_ID
                                INNER JOIN SYS.TABLES TAB1 ON TAB1.OBJECT_ID = FKC.PARENT_OBJECT_ID
                                INNER JOIN SYS.SCHEMAS SCH ON TAB1.SCHEMA_ID = SCH.SCHEMA_ID
                                INNER JOIN SYS.COLUMNS COL1 ON COL1.COLUMN_ID = PARENT_COLUMN_ID AND COL1.OBJECT_ID = TAB1.OBJECT_ID
                                INNER JOIN SYS.TABLES TAB2 ON TAB2.OBJECT_ID = FKC.REFERENCED_OBJECT_ID
                                INNER JOIN SYS.COLUMNS COL2 ON COL2.COLUMN_ID = REFERENCED_COLUMN_ID AND COL2.OBJECT_ID = TAB2.OBJECT_ID
                                     WHERE TAB1.NAME = @table";

            return _unitOfWork.Query<ForeignKey>(query, new {table});
        }
    }
}