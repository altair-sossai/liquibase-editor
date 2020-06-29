﻿using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes
{
    [XmlRoot(ElementName = "changeSet")]
    public class AddColumnChangeSet
    {
        [XmlElement(ElementName = "preConditions")]
        public PreConditionsElement PreConditions { get; set; }

        [XmlElement(ElementName = "addColumn")]
        public AddColumnElement AddColumn { get; set; }

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

        [XmlRoot(ElementName = "not")]
        public class NotElement
        {
            [XmlElement(ElementName = "columnExists")]
            public ColumnExistsElement ColumnExists { get; set; }
        }

        [XmlRoot(ElementName = "preConditions")]
        public class PreConditionsElement
        {
            [XmlElement(ElementName = "not")]
            public NotElement Not { get; set; }

            [XmlAttribute(AttributeName = "onFail")]
            public string OnFail { get; set; }

            [XmlAttribute(AttributeName = "onFailMessage")]
            public string OnFailMessage { get; set; }
        }

        [XmlRoot(ElementName = "column")]
        public class ColumnElement
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }
        }

        [XmlRoot(ElementName = "addColumn")]
        public class AddColumnElement
        {
            [XmlElement(ElementName = "column")]
            public ColumnElement Column { get; set; }

            [XmlAttribute(AttributeName = "schemaName")]
            public string SchemaName { get; set; }

            [XmlAttribute(AttributeName = "tableName")]
            public string TableName { get; set; }
        }
    }
}