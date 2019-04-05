using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class XmlExtensionsTests
    {
        [Test]
        public void StripNamespaces_Test()
        {
            // Arrange
            var ns = XNamespace.Get("http://test.name.space");
            var xml = new XElement("root",
                new XElement("elem1", "content1", new XAttribute("attr1", "value1")),
                new XElement(ns + "elem2", "content2", new XAttribute(ns + "attr2", "value2")),
                new XElement("elem3", new XElement(ns + "content3_elem1"), new XElement("content3_elem2")));

            // Act
            var strippedXml = xml.StripNamespaces();

            // Assert
            Assert.That(strippedXml, Is.Not.SameAs(xml));
            Assert.That(strippedXml.DescendantsAndSelf().Elements().Select(a => a.Name.NamespaceName), Has.All.Null.Or.Empty);
            Assert.That(strippedXml.DescendantsAndSelf().Attributes().Select(a => a.Name.NamespaceName), Has.All.Null.Or.Empty);
        }
    }
}