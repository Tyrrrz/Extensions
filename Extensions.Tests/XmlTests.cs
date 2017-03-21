using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class XmlTests
    {
        [TestMethod]
        public void StripNamespacesTest()
        {
            var ns = XNamespace.Get("http://schemas.domain.com/orders");
            var xml =
                new XElement(ns + "order",
                    new XElement(ns + "customer", "Foo", new XAttribute(ns + "hello", "world")),
                    new XElement("purchases",
                        new XElement(ns + "purchase", "Unicycle", new XAttribute("price", "100.00")),
                        new XElement("purchase", "Bicycle"),
                        new XElement(ns + "purchase", "Tricycle",
                            new XAttribute("price", "300.00"),
                            new XAttribute(XNamespace.Xml.GetName("space"), "preserve")
                        )
                    )
                );

            var stripped = xml.StripNamespaces();
            var xCustomer = stripped.Element("customer");
            var xHello = xCustomer?.Attribute("hello");

            Assert.AreNotSame(xml, stripped);
            Assert.IsNotNull(xCustomer);
            Assert.IsNotNull(xHello);
            Assert.AreEqual("world", xHello.Value);
        }
    }
}