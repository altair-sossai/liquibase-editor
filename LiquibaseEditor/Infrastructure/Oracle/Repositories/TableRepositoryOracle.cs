using System.Collections.Generic;
using System.Linq;
using LiquibaseEditor.Entities;
using LiquibaseEditor.Infrastructure.Oracle.Models;
using LiquibaseEditor.Infrastructure.Oracle.UnitOfWork;
using LiquibaseEditor.Repositories;

namespace LiquibaseEditor.Infrastructure.Oracle.Repositories
{
    public class TableRepositoryOracle : ITableRepository
    {
        private readonly UnitOfWorkOracleDatabase _unitOfWork;

        public TableRepositoryOracle(UnitOfWorkOracleDatabase unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<Table> GetAll()
        {
            const string query = @"SELECT TABLE_NAME AS Name FROM TABS ORDER BY TABLE_NAME";

            return _unitOfWork.Query<Table>(query);
        }

        public List<Column> GetColumns(string table)
        {
            const string query = @"SELECT COL.COLUMN_NAME AS Name,
                                     CASE COL.COLUMN_NAME WHEN 'ID' THEN 1 ELSE 0 END AS AutoIncrement,
                                     (SELECT CASE COUNT(1) WHEN 0 THEN 0 ELSE 1 END AS PrimaryKey
                                        FROM ALL_CONSTRAINTS CONS, ALL_CONS_COLUMNS COLS
                                        WHERE COLS.TABLE_NAME = :TableNameParam
                                        AND CONS.CONSTRAINT_NAME = COLS.CONSTRAINT_NAME
                                        AND CONS.OWNER = COLS.OWNER
                                        AND CONS.OWNER = USER
                                        AND CONS.CONSTRAINT_TYPE = 'P'
                                        AND COLS.COLUMN_NAME = COL.COLUMN_NAME) AS PrimaryKey,
                                     (SELECT CASE COUNT(1) WHEN 0 THEN 0 ELSE 1 END AS ForeignKey
                                        FROM ALL_CONSTRAINTS CONS, ALL_CONS_COLUMNS COLS
                                        WHERE COLS.TABLE_NAME = :TableNameParam
                                        AND CONS.CONSTRAINT_NAME = COLS.CONSTRAINT_NAME
                                        AND CONS.OWNER = COLS.OWNER
                                        AND CONS.OWNER = USER
                                        AND CONS.CONSTRAINT_TYPE = 'R'
                                        AND COLS.COLUMN_NAME = COL.COLUMN_NAME) AS ForeignKey,
                                     CASE COL.NULLABLE WHEN 'Y' THEN 1 ELSE 0 END AS Nullable,
                                     COL.DATA_TYPE AS DataType,
                                     COL.DATA_LENGTH AS Length,
                                     COL.DATA_PRECISION AS Precision,
                                     COL.DATA_SCALE AS Scale
                                FROM SYS.ALL_TAB_COLUMNS COL
                          INNER JOIN SYS.ALL_TABLES T ON COL.OWNER = T.OWNER AND COL.TABLE_NAME = T.TABLE_NAME
                               WHERE COL.OWNER = USER
                                 AND COL.TABLE_NAME = :TableNameParam
                            ORDER BY COL.COLUMN_ID";

            return _unitOfWork.Query<ColumnAdapter>(query, new {TableNameParam = table}).Cast<Column>().ToList();
        }

        public List<ForeignKey> GetForeignKeys(string table)
        {
            const string query = @"SELECT A.CONSTRAINT_NAME AS Name, 
                                          A.TABLE_NAME AS TableName, 
                                           A.COLUMN_NAME AS ColumnName, 
                                           C_PK.TABLE_NAME AS ReferencedTableName,
                                           B.COLUMN_NAME AS ReferencedColumnName
                                      FROM USER_CONS_COLUMNS A
                                      JOIN USER_CONSTRAINTS C ON A.OWNER = C.OWNER
                                                             AND A.CONSTRAINT_NAME = C.CONSTRAINT_NAME
                                      JOIN USER_CONSTRAINTS C_PK ON C.R_OWNER = C_PK.OWNER
                                                                AND C.R_CONSTRAINT_NAME = C_PK.CONSTRAINT_NAME
                                      JOIN USER_CONS_COLUMNS B ON C_PK.OWNER = B.OWNER
                                                              AND C_PK.CONSTRAINT_NAME = B.CONSTRAINT_NAME 
                                                              AND B.POSITION = A.POSITION     
                                     WHERE C.CONSTRAINT_TYPE = 'R' 
                                       AND A.TABLE_NAME = :TableNameParam";

            return _unitOfWork.Query<ForeignKey>(query, new {TableNameParam = table});
        }
    }
}