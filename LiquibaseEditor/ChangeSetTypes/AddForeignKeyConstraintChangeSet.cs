using System.Xml.Serialization;

namespace LiquibaseEditor.ChangeSetTypes
{
    [XmlRoot(ElementName = "changeSet")]
    public class AddForeignKeyConstraintChangeSet
    {
        [XmlElement(ElementName = "preConditions")]
        public PreConditionsElement PreConditions { get; set; }

        [XmlElement(ElementName = "addForeignKeyConstraint")]
        public AddForeignKeyConstraintElement AddForeignKeyConstraint { get; set; }

        [XmlAttribute(AttributeName = "id")]
        public string Id { get; set; }

        [XmlAttribute(AttributeName = "author")]
        public string Author { get; set; }

        [XmlRoot(ElementName = "foreignKeyConstraintExists")]
        public class ForeignKeyConstraintExistsElement
        {
            [XmlAttribute(AttributeName = "foreignKeyName")]
            public string ForeignKeyName { get; set; }
        }

        [XmlRoot(ElementName = "not")]
        public class NotElement
        {
            [XmlElement(ElementName = "foreignKeyConstraintExists")]
            public ForeignKeyConstraintExistsElement ForeignKeyConstraintExists { get; set; }
        }

        [XmlRoot(ElementName = "preConditions")]
        public class PreConditionsElement
        {
            [XmlElement(ElementName = "not")]
            public NotElement Not { get; set; }

            [XmlAttribute(AttributeName = "onFail")]
            public string OnFail { get; set; }
        }

        [XmlRoot(ElementName = "addForeignKeyConstraint")]
        public class AddForeignKeyConstraintElement
        {
            [XmlAttribute(AttributeName = "baseColumnNames")]
            public string BaseColumnNames { get; set; }

            [XmlAttribute(AttributeName = "baseTableName")]
            public string BaseTableName { get; set; }

            [XmlAttribute(AttributeName = "constraintName")]
            public string ConstraintName { get; set; }

            [XmlAttribute(AttributeName = "referencedTableName")]
            public string ReferencedTableName { get; set; }

            [XmlAttribute(AttributeName = "referencedColumnNames")]
            public string ReferencedColumnNames { get; set; }

            [XmlAttribute(AttributeName = "deferrable")]
            public string Deferrable { get; set; }

            [XmlAttribute(AttributeName = "initiallyDeferred")]
            public string InitiallyDeferred { get; set; }

            [XmlAttribute(AttributeName = "onDelete")]
            public string OnDelete { get; set; }

            [XmlAttribute(AttributeName = "onUpdate")]
            public string OnUpdate { get; set; }
        }
    }
}