using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes.Elements
{
    [XmlRoot(ElementName = "columnExists")]
    public class ColumnExistsElement
    {
        [XmlAttribute(AttributeName = "columnName")]
        public string ColumnName { get; set; }

        [XmlAttribute(AttributeName = "tableName")]
        public string TableName { get; set; }
    }
}