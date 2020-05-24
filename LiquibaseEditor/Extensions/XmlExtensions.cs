using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace LiquibaseEditor.Extensions
{
    public static class XmlExtensions
    {
        public static string ToXml<T>(this T t)
            where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("", "");

            var settings = new XmlWriterSettings {Indent = true};

            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, settings);
            xmlSerializer.Serialize(xmlWriter, t, namespaces);

            return stringWriter.ToString();
        }
    }
}