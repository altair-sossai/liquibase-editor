using LiquibaseEditor.Entities;
using LiquibaseEditor.Enums;

namespace LiquibaseEditor.Infrastructure.Oracle.Models
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

                switch (value?.ToUpper())
                {
                    case "NUMBER":
                        Type = ColumnType.Number;
                        break;

                    case "CHAR":
                        Type = ColumnType.Char;
                        break;

                    case "VARCHAR2":
                    case "NVARCHAR2":
                        Type = ColumnType.Varchar;
                        break;

                    case "DATE":
                        Type = ColumnType.DateTime;
                        break;
                }
            }
        }
    }
}