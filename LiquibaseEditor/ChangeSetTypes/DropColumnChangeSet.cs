using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes
{
    [XmlRoot(ElementName = "changeSet")]
    public class DropColumnChangeSet
    {
        [XmlElement(ElementName = "preConditions")]
        public PreConditionsElement PreConditions { get; set; }

        [XmlElement(ElementName = "dropColumn")]
        public DropColumnElement DropColumn { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }


        [XmlRoot(ElementName = "columnExists")]
        public class ColumnExistsElement
        {
            [XmlAttribute(AttributeName = "columnName")]
            public string ColumnName { get; set; }

            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { get; set; }
        }

        [XmlRoot(ElementName = "preConditions")]
        public class PreConditionsElement
        {
            [XmlElement(ElementName = "columnExists")]
            public ColumnExistsElement ColumnExists { get; set; }

            [XmlAttribute(AttributeName = "onFail")]
            public string OnFail { get; set; }
        }

        [XmlRoot(ElementName = "dropColumn")]
        public class DropColumnElement
        {
            [XmlAttribute(AttributeName = "columnName")]
            public string ColumnName { get; set; }

            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { get; set; }
        }
    }
}