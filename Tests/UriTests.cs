using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Extensions;

namespace Tests
{
    [TestClass]
    public class UriTests
    {
        [TestMethod]
        public void SetQueryParameterTest()
        {
            var a = "http://www.asd.com/?a=123&b=&c=ve&d";
            var b = "http://www.asd.com/";

            var aExisting = a.SetQueryParameter("a", "345");
            var aNew = a.SetQueryParameter("e", "xxx");
            var aExistingEmpty = a.SetQueryParameter("b", "ppp");
            var aExistingStub = a.SetQueryParameter("d", "ggg");
            var bNew = b.SetQueryParameter("qwe", "aaa");

            Assert.AreEqual("http://www.asd.com/?a=345&b=&c=ve&d", aExisting);
            Assert.AreEqual("http://www.asd.com/?a=123&b=&c=ve&d&e=xxx", aNew);
            Assert.AreEqual("http://www.asd.com/?a=123&b=ppp&c=ve&d", aExistingEmpty);
            Assert.AreEqual("http://www.asd.com/?a=123&b=&c=ve&d=ggg", aExistingStub);
            Assert.AreEqual("http://www.asd.com/?qwe=aaa", bNew);
        }
    }
}