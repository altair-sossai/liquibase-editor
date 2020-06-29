using System.Collections.Generic;
using System.Xml.Serialization;
using LiquibaseEditor.ChangeSetTypes.Elements;

namespace LiquibaseEditor.ChangeSetTypes
{
    [XmlRoot(ElementName = "changeSet")]
    public class CreateTableChangeSet
    {
        [XmlElement(ElementName = "preConditions")]
        public PreConditionsElement PreConditions { get; set; }

        [XmlElement(ElementName = "createTable")]
        public CreateTableElement CreateTable { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }


        [XmlRoot(ElementName = "tableExists")]
        public class TableExistsElement
        {
            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { get; set; }
        }

        [XmlRoot(ElementName = "not")]
        public class NotElement
        {
            [XmlElement(ElementName = "tableExists")]
            public TableExistsElement TableExists { get; set; }
        }

        [XmlRoot(ElementName = "preConditions")]
        public class PreConditionsElement
        {
            [XmlElement(ElementName = "not")]
            public NotElement Not { get; set; }

            [XmlAttribute(AttributeName = "onFail")]
            public string OnFail { get; set; }
        }

        [XmlRoot(ElementName = "createTable")]
        public class CreateTableElement
        {
            [XmlElement(ElementName = "column")]
            public List<ColumnElement> Column { get; set; }

            [XmlAttribute(AttributeName = "remarks")]
            public string Remarks { get; set; }

            [XmlAttribute(AttributeName = "schemaName")]
            public string SchemaName { get; set; }

            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { get; set; }
        }
    }
}