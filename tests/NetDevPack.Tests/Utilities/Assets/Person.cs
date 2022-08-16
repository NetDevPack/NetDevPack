using System.Xml.Serialization;

namespace NetDevPack.Tests.Utilities.Assets
{
    [XmlRoot]
    [XmlType("Person")]
    public class Person
    {
        [XmlElement("FirstName")]
        public string Name { get; set; }

        [XmlElement("RoughAge")]
        public int Age { get; set; }
    }
}