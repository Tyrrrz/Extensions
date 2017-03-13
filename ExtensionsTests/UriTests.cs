using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class UriTests
    {
        [TestMethod]
        public void SetQueryParameterTest()
        {
            string a = "http://www.asd.com/?a=123&b=&c=ve&d";
            string b = "http://www.asd.com/";

            string aExisting = a.SetQueryParameter("a", "345");
            string aNew = a.SetQueryParameter("e", "xxx");
            string aExistingEmpty = a.SetQueryParameter("b", "ppp");
            string aExistingStub = a.SetQueryParameter("d", "ggg");
            string bNew = b.SetQueryParameter("qwe", "aaa");

            Assert.AreEqual("http://www.asd.com/?a=345&b=&c=ve&d", aExisting);
            Assert.AreEqual("http://www.asd.com/?a=123&b=&c=ve&d&e=xxx", aNew);
            Assert.AreEqual("http://www.asd.com/?a=123&b=ppp&c=ve&d", aExistingEmpty);
            Assert.AreEqual("http://www.asd.com/?a=123&b=&c=ve&d=ggg", aExistingStub);
            Assert.AreEqual("http://www.asd.com/?qwe=aaa", bNew);
        }
    }
}