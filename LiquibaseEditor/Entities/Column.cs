using LiquibaseEditor.Enums;

namespace LiquibaseEditor.Entities
{
    public class Column
    {
        public string Name { get; set; }
        public bool AutoIncrement { get; set; }
        public bool PrimaryKey { get; set; }
        public bool ForeignKey { get; set; }
        public bool Nullable { get; set; }
        public ColumnType Type { get; set; }
        public int? Length { get; set; }
        public int? Precision { get; set; }
        public int? Scale { get; set; }
    }
}