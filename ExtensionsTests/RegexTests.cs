using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class RegexTests
    {
        [TestMethod]
        public void MatchOrNullTest()
        {
            var regex = new Regex(@"value1=(?<g1>.+?)&value2=(?<g2>.+?)$");

            string im = "value1=asd&value2=qwe";
            string imn = "hello world";

            string m = regex.MatchOrNull(im);
            string mgi = regex.MatchOrNull(im, 2);
            string mgn = regex.MatchOrNull(im, "g2");
            string mn = regex.MatchOrNull(imn);
            string mngi = regex.MatchOrNull(imn, 2);
            string mngn = regex.MatchOrNull(imn, "g2");

            Assert.AreEqual("value1=asd&value2=qwe", m);
            Assert.AreEqual("qwe", mgi);
            Assert.AreEqual("qwe", mgn);
            Assert.IsNull(mn);
            Assert.IsNull(mngi);
            Assert.IsNull(mngn);
        }
    }
}