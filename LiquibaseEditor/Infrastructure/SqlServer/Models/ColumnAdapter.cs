using LiquibaseEditor.Entities;
using LiquibaseEditor.Enums;

namespace LiquibaseEditor.Infrastructure.SqlServer.Models
{
    public class ColumnAdapter : Column
    {
        private string _dataType;

        public string DataType
        {
            get => _dataType;
            set
            {
                _dataType = value;

                switch (value)
                {
                    case "bit":
                        Type = ColumnType.Bit;
                        break;

                    case "int":
                        Type = ColumnType.Int;
                        break;

                    case "bigint":
                        Type = ColumnType.Long;
                        break;

                    case "real":
                    case "numeric":
                    case "decimal":
                        Type = ColumnType.Number;
                        break;

                    case "char":
                        Type = ColumnType.Char;
                        break;

                    case "varchar":
                        Type = ColumnType.Varchar;
                        break;

                    case "uniqueidentifier":
                        Type = ColumnType.Guid;
                        break;

                    case "date":
                    case "datetime":
                        Type = ColumnType.DateTime;
                        break;
                }
            }
        }
    }
}