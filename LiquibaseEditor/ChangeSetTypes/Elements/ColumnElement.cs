using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes.Elements
{
    [XmlRoot(ElementName = "column")]
    public class ColumnElement
    {
        [XmlElement(ElementName = "constraints")]
        public ConstraintsElement Constraints { get; set; }

        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }

        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }

        [XmlAttribute(AttributeName = "autoIncrement")]
        public string AutoIncrement { get; set; }

        [XmlAttribute(AttributeName = "remarks")]
        public string Remarks { get; set; }
    }
}