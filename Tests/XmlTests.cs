using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Extensions;
using Tyrrrz.Extensions.Exceptions;

namespace Tests
{
    [TestClass]
    public class XmlTests
    {
        private XElement GetDummyXml()
        {
            var ns = XNamespace.Get("http://test.name.space");
            var xml =
                new XElement("root",
                    new XElement("elem1", "hello world", new XAttribute("attr1", "hello universe")),
                    new XElement(ns + "elem2", "bye world", new XAttribute(ns + "attr2", "bye universe")),
                    new XElement("elem3", new XElement("elem3c"), new XElement("elem3c"))
                );

            return xml;
        }

        [TestMethod]
        public void StripNamespacesTest()
        {
            var xml = GetDummyXml();

            var stripped = xml.StripNamespaces();
            var element = stripped.Element("elem2");
            var attribute = element?.Attribute("attr2");

            Assert.AreNotSame(xml, stripped);
            Assert.IsNotNull(element);
            Assert.IsNotNull(attribute);
        }

        [TestMethod]
        public void DescendantTest()
        {
            var xml = GetDummyXml();

            var descendant = xml.Descendant("elem3c");
            var descendantNotExisting = xml.Descendant("qwerty");

            Assert.IsNotNull(descendant);
            Assert.AreEqual("elem3c", descendant.Name.LocalName);
            Assert.IsNull(descendantNotExisting);
        }

        [TestMethod]
        public void DescendantStrictTest()
        {
            var xml = GetDummyXml();

            var descendant = xml.DescendantStrict("elem3c");

            Assert.IsNotNull(descendant);
            Assert.AreEqual("elem3c", descendant.Name.LocalName);

            Assert.ThrowsException<XmlElementNotFoundException>(() => xml.DescendantStrict("qwerty"));
        }

        [TestMethod]
        public void ElementStrictTest()
        {
            var xml = GetDummyXml();

            var element = xml.ElementStrict("elem1");

            Assert.IsNotNull(element);
            Assert.AreEqual("elem1", element.Name.LocalName);

            Assert.ThrowsException<XmlElementNotFoundException>(() => xml.ElementStrict("qwerty"));
        }

        [TestMethod]
        public void AttributeStrictTest()
        {
            var xml = GetDummyXml();

            var element = xml.Element("elem1");
            var attribute = element?.AttributeStrict("attr1");

            Assert.IsNotNull(attribute);
            Assert.AreEqual("attr1", attribute.Name.LocalName);

            Assert.ThrowsException<XmlElementNotFoundException>(() => element.AttributeStrict("qwerty"));
        }
    }
}