using System.Collections.Generic;
using System.Xml.Serialization;

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