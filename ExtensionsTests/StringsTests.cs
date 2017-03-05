using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class StringsTests
    {
        [TestMethod]
        public void IsBlankTest()
        {
            Assert.IsTrue("".IsBlank());
            Assert.IsTrue(((string)null).IsBlank());
            Assert.IsTrue(" ".IsBlank());
            Assert.IsTrue("    ".IsBlank());
            Assert.IsFalse("asd".IsBlank());
        }

        [TestMethod]
        public void IsNotBlankTest()
        {
            Assert.IsFalse("".IsNotBlank());
            Assert.IsFalse(((string) null).IsNotBlank());
            Assert.IsFalse(" ".IsNotBlank());
            Assert.IsFalse("    ".IsNotBlank());
            Assert.IsTrue("asd".IsNotBlank());
        }

        [TestMethod]
        public void IsNumericTest()
        {
            Assert.IsTrue("123345".IsNumeric());
            Assert.IsFalse("123asd".IsNumeric());
            Assert.IsFalse("asd".IsNumeric());
        }

        [TestMethod]
        public void NullIfBlankTest()
        {
            Assert.IsNull("".NullIfBlank());
            Assert.IsNull("  ".NullIfBlank());
            Assert.IsNull(((string) null).NullIfBlank());
            Assert.IsNotNull("qwe".NullIfBlank());
        }

        [TestMethod]
        public void FormatTest()
        {
            Assert.AreEqual("{0}".Format(123), $"{123}");
        }

        [TestMethod]
        public void ReverseTest()
        {
            string str = "Hello World";

            string sr = str.Reverse();

            Assert.AreEqual(str.Length, sr.Length);
            for (int i = 0; i < str.Length; i++)
                Assert.AreEqual(str[i], sr[sr.Length - 1 - i]);
        }

        [TestMethod]
        public void RepeatTest()
        {
            string r = 'a'.Repeat(3);
            string rs = "qwe".Repeat(2);
            string ro = "asd".Repeat(1);
            string rn = "asd".Repeat(0);

            Assert.AreEqual("aaa", r);
            Assert.AreEqual("qweqwe", rs);
            Assert.AreEqual("asd", ro);
            Assert.AreEqual("", rn);
        }

        [TestMethod]
        public void TakeTest()
        {
            string str = "Hello World";

            string t = str.Take(5);
            string tl = str.Take(0);
            string tm = str.Take(123);

            Assert.AreEqual("Hello", t);
            Assert.AreEqual("", tl);
            Assert.AreEqual("Hello World", tm);
        }

        [TestMethod]
        public void SkipTest()
        {
            string str = "Hello World";

            string s = str.Skip(5);
            string sl = str.Skip(0);
            string sm = str.Skip(123);

            Assert.AreEqual(" World", s);
            Assert.AreEqual("Hello World", sl);
            Assert.AreEqual("", sm);
        }

        [TestMethod]
        public void TakeLastTest()
        {
            string str = "Hello World";

            string t = str.TakeLast(5);
            string tl = str.TakeLast(0);
            string tm = str.TakeLast(123);

            Assert.AreEqual("World", t);
            Assert.AreEqual("", tl);
            Assert.AreEqual("Hello World", tm);
        }

        [TestMethod]
        public void SkipLastTest()
        {
            string str = "Hello World";

            string s = str.SkipLast(5);
            string sl = str.SkipLast(0);
            string sm = str.SkipLast(123);

            Assert.AreEqual("Hello ", s);
            Assert.AreEqual("Hello World", sl);
            Assert.AreEqual("", sm);
        }

        [TestMethod]
        public void WithoutTest()
        {
            string str = "Hello World";

            string w = str.Without("He", " ", "d");
            string wc = str.Without('H', 'o');

            Assert.AreEqual("lloWorl", w);
            Assert.AreEqual("ell Wrld", wc);
        }

        [TestMethod]
        public void EnsureEndsWithTest()
        {
            string str = "asd";

            string ew = str.EnsureEndsWith("qqq");
            string ewa = str.EnsureEndsWith("sd");

            Assert.AreEqual("asdqqq", ew);
            Assert.AreEqual("asd", ewa);
        }

        [TestMethod]
        public void EnsureStartsWithTest()
        {
            string str = "asd";

            string sw = str.EnsureStartsWith("qqq");
            string swa = str.EnsureStartsWith("as");

            Assert.AreEqual("qqqasd", sw);
            Assert.AreEqual("asd", swa);
        }

        [TestMethod]
        public void SubstringUntilTest()
        {
            string str = "qwe=asd=zxc=cvb";

            string su = str.SubstringUntil("=");

            Assert.AreEqual("qwe", su);
        }

        [TestMethod]
        public void SubstringAfterTest()
        {
            string str = "qwe=asd=zxc=cvb";

            string sa = str.SubstringAfter("=");

            Assert.AreEqual("asd=zxc=cvb", sa);
        }

        [TestMethod]
        public void WithoutBlankTest()
        {
            var a = new[] {"asd", "  ", "", null, "qwe"};

            var wb = a.WithoutBlank().ToArray();

            Assert.AreEqual(2, wb.Length);
            Assert.AreEqual("asd", wb[0]);
            Assert.AreEqual("qwe", wb[1]);
        }

        [TestMethod]
        public void ContainsInvariantTest()
        {
            var a = new[] { "asd", "ASD", "xxx", "qwe" };

            Assert.IsTrue(a.ContainsInvariant("asd"));
            Assert.IsTrue(a.ContainsInvariant("XxX"));
            Assert.IsFalse(a.ContainsInvariant("123"));
        }

        [TestMethod]
        public void StripCommonStartTest()
        {
            var a = new[] { "asd213", "asd44", "asdzxc" };

            var scs = a.StripCommonStart().ToArray();

            Assert.AreEqual("213", scs[0]);
            Assert.AreEqual("44", scs[1]);
            Assert.AreEqual("zxc", scs[2]);
        }

        [TestMethod]
        public void StripCommonEndTest()
        {
            var a = new[] { "213qw", "44qw", "zxcqw" };

            var scs = a.StripCommonEnd().ToArray();

            Assert.AreEqual("213", scs[0]);
            Assert.AreEqual("44", scs[1]);
            Assert.AreEqual("zxc", scs[2]);
        }

        [TestMethod]
        public void EqualsInvariantTest()
        {
            Assert.IsTrue("asd".EqualsInvariant("asd"));
            Assert.IsTrue("asd".EqualsInvariant(" asd"));
            Assert.IsTrue("asd".EqualsInvariant("ASD"));
            Assert.IsTrue("asd".EqualsInvariant("ASD  "));
            Assert.IsTrue("".EqualsInvariant(""));
            Assert.IsFalse("asd".EqualsInvariant("qwe"));
        }

        [TestMethod]
        public void ContainsWordTest()
        {
            Assert.IsTrue("qwe asd zxc".ContainsWord("asd"));
            Assert.IsTrue("qwe asd zxc".ContainsWord("asd zxc"));
            Assert.IsFalse("qwe asd zxc".ContainsWord("qw"));
        }
    }
}