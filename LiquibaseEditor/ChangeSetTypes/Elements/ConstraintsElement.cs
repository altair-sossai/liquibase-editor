using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes.Elements
{
    [XmlRoot(ElementName = "constraints")]
    public class ConstraintsElement
    {
        [XmlAttribute(AttributeName = "primaryKey")]
        public string PrimaryKey { get; set; }

        [XmlAttribute(AttributeName = "primaryKeyName")]
        public string PrimaryKeyName { get; set; }

        [XmlAttribute(AttributeName = "nullable")]
        public string Nullable { get; set; }
    }
}