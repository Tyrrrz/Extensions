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
            Assert.IsTrue(((string) null).IsBlank());
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
            Assert.IsFalse("!##$".IsNumeric());
        }

        [TestMethod]
        public void IsAlphabeticTest()
        {
            Assert.IsFalse("123345".IsAlphabetic());
            Assert.IsFalse("123asd".IsAlphabetic());
            Assert.IsTrue("asd".IsAlphabetic());
            Assert.IsFalse("!##$".IsAlphabetic());
        }

        [TestMethod]
        public void IsAlphanumericTest()
        {
            Assert.IsTrue("123345".IsAlphanumeric());
            Assert.IsTrue("123asd".IsAlphanumeric());
            Assert.IsTrue("asd".IsAlphanumeric());
            Assert.IsFalse("!##$".IsAlphanumeric());
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
        public void ContainsInvariantTest()
        {
            Assert.IsTrue("qwe asd zxc".ContainsInvariant("asd"));
            Assert.IsTrue("qwe ASD zxc".ContainsInvariant("asd"));
            Assert.IsTrue("asd".ContainsInvariant("ASD"));
            Assert.IsTrue("asd".ContainsInvariant("ASD  "));
            Assert.IsFalse("asd 123 ggg".ContainsInvariant("qwe"));
        }

        [TestMethod]
        public void ContainsWordTest()
        {
            Assert.IsTrue("qwe asd zxc".ContainsWord("asd"));
            Assert.IsTrue("qwe asd zxc".ContainsWord("asd zxc"));
            Assert.IsFalse("qwe asd zxc".ContainsWord("qw"));
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
        public void EmptyIfNullTest()
        {
            Assert.AreEqual("asd", "asd".EmptyIfNull());
            Assert.AreEqual("  ", "  ".EmptyIfNull());
            Assert.AreEqual("", "".EmptyIfNull());
            Assert.AreEqual("", ((string) null).EmptyIfNull());
        }

        [TestMethod]
        public void EmptyIfBlankTest()
        {
            Assert.AreEqual("asd", "asd".EmptyIfBlank());
            Assert.AreEqual("", "  ".EmptyIfBlank());
            Assert.AreEqual("", "".EmptyIfBlank());
            Assert.AreEqual("", ((string) null).EmptyIfBlank());
        }

        [TestMethod]
        public void FormatTest()
        {
            Assert.AreEqual("{0}".Format(123), $"{123}");
        }

        [TestMethod]
        public void ReverseTest()
        {
            string s = "Hello World";

            string sReverse = s.Reverse();

            Assert.AreEqual(s.Length, sReverse.Length);
            for (int i = 0; i < s.Length; i++)
                Assert.AreEqual(s[i], sReverse[sReverse.Length - 1 - i]);
        }

        [TestMethod]
        public void RepeatTest()
        {
            string repeat = "qwe".Repeat(2);
            string repeatChar = 'a'.Repeat(3);
            string repeatOnce = "asd".Repeat(1);
            string repeatZero = "asd".Repeat(0);

            Assert.AreEqual("qweqwe", repeat);
            Assert.AreEqual("aaa", repeatChar);
            Assert.AreEqual("asd", repeatOnce);
            Assert.AreEqual("", repeatZero);
        }

        [TestMethod]
        public void TakeTest()
        {
            string s = "Hello World";

            string sTake = s.Take(5);
            string sTakeZero = s.Take(0);
            string sTakeTooMany = s.Take(123);

            Assert.AreEqual("Hello", sTake);
            Assert.AreEqual("", sTakeZero);
            Assert.AreEqual("Hello World", sTakeTooMany);
        }

        [TestMethod]
        public void SkipTest()
        {
            string s = "Hello World";

            string sSkip = s.Skip(5);
            string sSkipZero = s.Skip(0);
            string sSkipTooMany = s.Skip(123);

            Assert.AreEqual(" World", sSkip);
            Assert.AreEqual("Hello World", sSkipZero);
            Assert.AreEqual("", sSkipTooMany);
        }

        [TestMethod]
        public void TakeLastTest()
        {
            string s = "Hello World";

            string sTakeLast = s.TakeLast(5);
            string sTakeLastZero = s.TakeLast(0);
            string sTakeLastTooMany = s.TakeLast(123);

            Assert.AreEqual("World", sTakeLast);
            Assert.AreEqual("", sTakeLastZero);
            Assert.AreEqual("Hello World", sTakeLastTooMany);
        }

        [TestMethod]
        public void SkipLastTest()
        {
            string s = "Hello World";

            string sSkipLast = s.SkipLast(5);
            string sSkipLastZero = s.SkipLast(0);
            string sSkipLastTooMany = s.SkipLast(123);

            Assert.AreEqual("Hello ", sSkipLast);
            Assert.AreEqual("Hello World", sSkipLastZero);
            Assert.AreEqual("", sSkipLastTooMany);
        }

        [TestMethod]
        public void ExceptTest()
        {
            string s = "Hello World";

            string sExcept = s.Except("He", " ", "d");
            string sExceptChar = s.Except('H', 'o');

            Assert.AreEqual("lloWorl", sExcept);
            Assert.AreEqual("ell Wrld", sExceptChar);
        }

        [TestMethod]
        public void EnsureEndsWithTest()
        {
            string s = "asd";

            string sEnsureEndsWith = s.EnsureEndsWith("qqq");
            string sEnsureEndsWithObsolete = s.EnsureEndsWith("sd");

            Assert.AreEqual("asdqqq", sEnsureEndsWith);
            Assert.AreEqual("asd", sEnsureEndsWithObsolete);
        }

        [TestMethod]
        public void EnsureStartsWithTest()
        {
            string s = "asd";

            string sEnsureStartsWith = s.EnsureStartsWith("qqq");
            string sEnsureStartsWithObsolete = s.EnsureStartsWith("as");

            Assert.AreEqual("qqqasd", sEnsureStartsWith);
            Assert.AreEqual("asd", sEnsureStartsWithObsolete);
        }

        [TestMethod]
        public void SubstringUntilTest()
        {
            string s = "qwe=asd=zxc=cvb";

            string sSubstringUntil = s.SubstringUntil("=");

            Assert.AreEqual("qwe", sSubstringUntil);
        }

        [TestMethod]
        public void SubstringAfterTest()
        {
            string s = "qwe=asd=zxc=cvb";

            string sSubstringAfter = s.SubstringAfter("=");

            Assert.AreEqual("asd=zxc=cvb", sSubstringAfter);
        }

        [TestMethod]
        public void SubstringUntilLastTest()
        {
            string s = "qwe=asd=zxc=cvb";

            string sSubstringUntilLast = s.SubstringUntilLast("=");

            Assert.AreEqual("qwe=asd=zxc", sSubstringUntilLast);
        }

        [TestMethod]
        public void SubstringAfterLastTest()
        {
            string s = "qwe=asd=zxc=cvb";

            string sSubstringAfterLast = s.SubstringAfterLast("=");

            Assert.AreEqual("cvb", sSubstringAfterLast);
        }

        [TestMethod]
        public void EnumerableContainsInvariantTest()
        {
            var a = new[] {"asd", "ASD", "xxx", "qwe"};

            Assert.IsTrue(a.ContainsInvariant("asd"));
            Assert.IsTrue(a.ContainsInvariant("XxX"));
            Assert.IsFalse(a.ContainsInvariant("123"));
        }

        [TestMethod]
        public void ExceptBlankTest()
        {
            var a = new[] {"asd", "  ", "", null, "qwe"};

            var aExceptBlank = a.ExceptBlank().ToArray();

            Assert.AreEqual(2, aExceptBlank.Length);
            CollectionAssert.AreEqual(new[] {"asd", "qwe"}, aExceptBlank);
        }

        [TestMethod]
        public void SplitTest()
        {
            string a = "asd<>qwe<>ert[]zzz";
            string b = "asd;qwe|ert:zzz";

            var aSplit = a.Split("<>", "[]");
            var bSplit = b.Split(';', '|', ':');

            CollectionAssert.AreEqual(new[] {"asd", "qwe", "ert", "zzz"}, aSplit);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "ert", "zzz"}, bSplit);
        }

        [TestMethod]
        public void JoinToStringTest()
        {
            var a = new[] {1, 2, 3, 4, 5};

            string aJoinToString = a.JoinToString();

            Assert.AreEqual("1, 2, 3, 4, 5", aJoinToString);
        }
    }
}