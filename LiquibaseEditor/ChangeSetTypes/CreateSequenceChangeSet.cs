using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes
{
    [XmlRoot(ElementName = "changeSet")]
    public class CreateSequenceChangeSet
    {
        [XmlElement(ElementName = "preConditions")]
        public PreConditionsElement PreConditions { get; set; }

        [XmlElement(ElementName = "createSequence")]
        public CreateSequenceElement CreateSequence { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "dbms")]
        public string Dbms { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }


        [XmlRoot(ElementName = "sqlCheck")]
        public class SqlCheckElement
        {
            [XmlAttribute(AttributeName = "expectedResult")]
            public string ExpectedResult { get; set; }

            [XmlText]
            public string Text { get; set; }
        }

        [XmlRoot(ElementName = "preConditions")]
        public class PreConditionsElement
        {
            [XmlElement(ElementName = "sqlCheck")]
            public SqlCheckElement SqlCheck { get; set; }

            [XmlAttribute(AttributeName = "onFail")]
            public string OnFail { get; set; }
        }

        [XmlRoot(ElementName = "createSequence")]
        public class CreateSequenceElement
        {
            [XmlAttribute(AttributeName = "cycle")]
            public string Cycle { get; set; }

            [XmlAttribute(AttributeName = "incrementBy")]
            public string IncrementBy { get; set; }

            [XmlAttribute(AttributeName = "maxValue")]
            public string MaxValue { get; set; }

            [XmlAttribute(AttributeName = "minValue")]
            public string MinValue { get; set; }

            [XmlAttribute(AttributeName = "ordered")]
            public string Ordered { get; set; }

            [XmlAttribute(AttributeName = "schemaName")]
            public string SchemaName { get; set; }

            [XmlAttribute(AttributeName = "sequenceName")]
            public string SequenceName { get; set; }

            [XmlAttribute(AttributeName = "startValue")]
            public string StartValue { get; set; }
        }
    }
}