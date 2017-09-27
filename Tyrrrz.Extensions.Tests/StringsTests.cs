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
        public void TrimStartTest()
        {
            var a = "...Hello...";
            var b = ".......!Hello!.......";
            var c = "Hello.";

            var aTrimStart = a.TrimStart("...");
            var bTrimStart = b.TrimStart("...");
            var cTrimStart = c.TrimStart("...");

            Assert.AreEqual("Hello...", aTrimStart);
            Assert.AreEqual(".!Hello!.......", bTrimStart);
            Assert.AreEqual("Hello.", cTrimStart);
        }

        [TestMethod]
        public void TrimEndTest()
        {
            var a = "...Hello...";
            var b = ".......!Hello!.......";
            var c = "Hello.";

            var aTrimEnd = a.TrimEnd("...");
            var bTrimEnd = b.TrimEnd("...");
            var cTrimEnd = c.TrimEnd("...");

            Assert.AreEqual("...Hello", aTrimEnd);
            Assert.AreEqual(".......!Hello!.", bTrimEnd);
            Assert.AreEqual("Hello.", cTrimEnd);
        }

        [TestMethod]
        public void TrimTest()
        {
            var a = "...Hello...";
            var b = ".......!Hello!.......";
            var c = "Hello.";

            var aTrim = a.Trim("...");
            var bTrim = b.Trim("...");
            var cTrim = c.Trim("...");

            Assert.AreEqual("Hello", aTrim);
            Assert.AreEqual(".!Hello!.", bTrim);
            Assert.AreEqual("Hello.", cTrim);
        }

        [TestMethod]
        public void ReverseTest()
        {
            var a = "Hello World";
            var b = "H";
            var c = "";

            var aReverse = a.Reverse();
            var bReverse = b.Reverse();
            var cReverse = c.Reverse();

            Assert.AreEqual("dlroW olleH", aReverse);
            Assert.AreEqual("H", bReverse);
            Assert.AreEqual("", cReverse);
        }

        [TestMethod]
        public void RepeatTest()
        {
            var repeat = "qwe".Repeat(2);
            var repeatChar = 'a'.Repeat(3);
            var repeatOnce = "asd".Repeat(1);
            var repeatZero = "asd".Repeat(0);

            Assert.AreEqual("qweqwe", repeat);
            Assert.AreEqual("aaa", repeatChar);
            Assert.AreEqual("asd", repeatOnce);
            Assert.AreEqual("", repeatZero);
        }

        [TestMethod]
        public void ReplaceTest()
        {
            var s = "Hello World";

            var sReplace = s.Replace(new[] {"H", "llo"}, "x");
            var sReplaceChars = s.Replace(new[] {'e', 'd'}, 'x');

            Assert.AreEqual("xex World", sReplace);
            Assert.AreEqual("Hxllo Worlx", sReplaceChars);
        }

        [TestMethod]
        public void EnsureStartsWithTest()
        {
            var s = "asd";

            var sEnsureStartsWith = s.EnsureStartsWith("qqq");
            var sEnsureStartsWithObsolete = s.EnsureStartsWith("as");

            Assert.AreEqual("qqqasd", sEnsureStartsWith);
            Assert.AreEqual("asd", sEnsureStartsWithObsolete);
        }

        [TestMethod]
        public void EnsureEndsWithTest()
        {
            var s = "asd";

            var sEnsureEndsWith = s.EnsureEndsWith("qqq");
            var sEnsureEndsWithObsolete = s.EnsureEndsWith("sd");

            Assert.AreEqual("asdqqq", sEnsureEndsWith);
            Assert.AreEqual("asd", sEnsureEndsWithObsolete);
        }

        [TestMethod]
        public void SubstringUntilTest()
        {
            var s = "qwe==asd==zxc==cvb";

            var sSubstringUntil = s.SubstringUntil("==");

            Assert.AreEqual("qwe", sSubstringUntil);
        }

        [TestMethod]
        public void SubstringAfterTest()
        {
            var s = "qwe==asd==zxc==cvb";

            var sSubstringAfter = s.SubstringAfter("==");

            Assert.AreEqual("asd==zxc==cvb", sSubstringAfter);
        }

        [TestMethod]
        public void SubstringUntilLastTest()
        {
            var s = "qwe==asd==zxc==cvb";

            var sSubstringUntilLast = s.SubstringUntilLast("==");

            Assert.AreEqual("qwe==asd==zxc", sSubstringUntilLast);
        }

        [TestMethod]
        public void SubstringAfterLastTest()
        {
            var s = "qwe==asd==zxc==cvb";

            var sSubstringAfterLast = s.SubstringAfterLast("==");

            Assert.AreEqual("cvb", sSubstringAfterLast);
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
            var a = "asd<>qwe<>ert[]zzz";
            var b = "asd;qwe|ert:zzz";

            var aSplit = a.Split("<>", "[]");
            var bSplit = b.Split(';', '|', ':');

            CollectionAssert.AreEqual(new[] {"asd", "qwe", "ert", "zzz"}, aSplit);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "ert", "zzz"}, bSplit);
        }

        [TestMethod]
        public void JoinToStringTest()
        {
            var a = new[] {1, 2, 3, 4, 5};

            var aJoinToString = a.JoinToString(", ");

            Assert.AreEqual("1, 2, 3, 4, 5", aJoinToString);
        }
    }
}