using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class UriTests
    {
        [TestMethod]
        public void SetQueryParameterTest()
        {
            string q = "http://www.asd.com/?a=123&b=&c=ve";
            string qnv = "http://www.asd.com/?a=123&b&c=ve";
            string qe = "http://www.asd.com/";
            string qer = "http://www.asd.com/path/";

            string nqr = q.SetQueryParameter("a", "345");
            string nqa = q.SetQueryParameter("d", "xxx");
            string nqre = q.SetQueryParameter("b", "ppp");
            string qnvre = qnv.SetQueryParameter("b", "ggg");
            string qenqa = qe.SetQueryParameter("qwe", "aaa");
            string qera = qer.SetQueryParameter("a", "qop");

            Assert.AreEqual("http://www.asd.com/?a=345&b=&c=ve", nqr);
            Assert.AreEqual("http://www.asd.com/?a=123&b=&c=ve&d=xxx", nqa);
            Assert.AreEqual("http://www.asd.com/?a=123&b=ppp&c=ve", nqre);
            Assert.AreEqual("http://www.asd.com/?a=123&b=ggg&c=ve", qnvre);
            Assert.AreEqual("http://www.asd.com/?qwe=aaa", qenqa);
            Assert.AreEqual("http://www.asd.com/path/?a=qop", qera);
        }
    }
}