using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [TestCase(null, "")]
        [TestCase("test", "test")]
        [TestCase(" ", " ")]
        public void EmptyIfNull_Test(string input, string output)
        {
            Assert.That(input.EmptyIfNull(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("", true)]
        [TestCase("  ", false)]
        [TestCase("test", false)]
        [TestCase(" test", false)]
        public void IsEmpty_Test(string input, bool output)
        {
            Assert.That(input.IsEmpty(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("", true)]
        [TestCase("  ", true)]
        [TestCase("test", false)]
        [TestCase(" test", false)]
        public void IsWhiteSpace_Test(string input, bool output)
        {
            Assert.That(input.IsWhiteSpace(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("123", true)]
        [TestCase("test", false)]
        [TestCase("123.5", false)]
        public void IsNumeric_Test(string input, bool output)
        {
            Assert.That(input.IsNumeric(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", true)]
        [TestCase("123", false)]
        [TestCase("test123", false)]
        public void IsAlphabetic_Test(string input, bool output)
        {
            Assert.That(input.IsAlphabetic(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("123", true)]
        [TestCase("test", true)]
        [TestCase("test123", true)]
        [TestCase("123.5", false)]
        public void IsAlphanumeric_Test(string input, bool output)
        {
            Assert.That(input.IsAlphanumeric(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", "te", "st")]
        [TestCase("test", "st", "test")]
        [TestCase("tetetetest", "te", "st")]
        [TestCase("tESt", "te", "St", StringComparison.OrdinalIgnoreCase)]
        public void TrimStart_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.TrimStart(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", "st", "te")]
        [TestCase("test", "te", "test")]
        [TestCase("testststst", "st", "te")]
        [TestCase("tESt", "st", "tE", StringComparison.OrdinalIgnoreCase)]
        public void TrimEnd_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.TrimEnd(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", "t", "es")]
        [TestCase("test", "es", "test")]
        [TestCase("tetesttete", "te", "st")]
        [TestCase("teTestTete", "te", "st", StringComparison.OrdinalIgnoreCase)]
        public void Trim_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.Trim(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", "tset")]
        [TestCase("", "")]
        [TestCase("t", "t")]
        public void Reverse_Test(string input, string output)
        {
            Assert.That(input.Reverse(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("test", 3, "testtesttest")]
        [TestCase("test", 1, "test")]
        [TestCase("test", 0, "")]
        public void Repeat_Test(string input, int count, string output)
        {
            Assert.That(input.Repeat(count), Is.EqualTo(output));
        }

        [Test]
        [TestCase('a', 3, "aaa")]
        [TestCase('a', 1, "a")]
        [TestCase('a', 0, "")]
        public void Repeat_Test(char input, int count, string output)
        {
            Assert.That(input.Repeat(count), Is.EqualTo(output));
        }

        [Test]
        [TestCase("aaatestbbtest", "test", "xyz", "aaaxyzbbxyz")]
        [TestCase("aaatestbb", "test", "xyz", "aaaxyzbb")]
        [TestCase("aaabbc", "test", "xyz", "aaabbc")]
        [TestCase("aaaTESTbbTeStc", "test", "xyz", "aaaxyzbbxyzc", StringComparison.OrdinalIgnoreCase)]
        public void Replace_Test(string input, string oldValue, string newValue, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.Replace(oldValue, newValue, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("aaatestbbb", "test", "aaa")]
        [TestCase("aaatestbbbtestccc", "test", "aaa")]
        [TestCase("aaatestbbbtestccc", "xyz", "aaatestbbbtestccc")]
        [TestCase("testaaa", "test", "")]
        [TestCase("aaatEStbbb", "test", "aaa", StringComparison.OrdinalIgnoreCase)]
        public void SubstringUntil_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.SubstringUntil(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("aaatestbbb", "test", "bbb")]
        [TestCase("aaatestbbbtestccc", "test", "bbbtestccc")]
        [TestCase("aaatestbbbtestccc", "xyz", "")]
        [TestCase("aaatest", "test", "")]
        [TestCase("aaatEStbbb", "test", "bbb", StringComparison.OrdinalIgnoreCase)]
        public void SubstringAfter_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.SubstringAfter(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("aaatestbbb", "test", "aaa")]
        [TestCase("aaatestbbbtestccc", "test", "aaatestbbb")]
        [TestCase("aaatestbbbtestccc", "xyz", "aaatestbbbtestccc")]
        [TestCase("testaaa", "test", "")]
        [TestCase("aaatEStbbb", "test", "aaa", StringComparison.OrdinalIgnoreCase)]
        public void SubstringUntilLast_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.SubstringUntilLast(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase("aaatestbbb", "test", "bbb")]
        [TestCase("aaatestbbbtestccc", "test", "ccc")]
        [TestCase("aaatestbbbtestccc", "xyz", "")]
        [TestCase("aaatest", "test", "")]
        [TestCase("aaatEStbbb", "test", "bbb", StringComparison.OrdinalIgnoreCase)]
        public void SubstringAfterLast_Test(string input, string sub, string output,
            StringComparison comparison = StringComparison.Ordinal)
        {
            Assert.That(input.SubstringAfterLast(sub, comparison), Is.EqualTo(output));
        }

        [Test]
        [TestCase(new[] { "aaa", "", " ", "bbb" }, new[] { "aaa", "bbb" })]
        public void ExceptEmptyOrWhiteSpace_Test(IEnumerable<string> input, IEnumerable<string> output)
        {
            Assert.That(input.ExceptEmptyOrWhiteSpace(), Is.EqualTo(output));
        }

        [Test]
        [TestCase("testaaatestaaatestaaa", new[] { "aaa" }, new[] { "test", "test", "test" })]
        public void Split_Test(string input, string[] separators, string[] output)
        {
            Assert.That(input.Split(separators), Is.EqualTo(output));
        }

        [Test]
        [TestCase("testaatestbbbtesta", new[] { 'a', 'b' }, new[] { "test", "test", "test" })]
        public void Split_Test(string input, char[] separators, string[] output)
        {
            // Using fully qualified name because it defaults to member method
            Assert.That(StringExtensions.Split(input, separators), Is.EqualTo(output));
        }

        [Test]
        [TestCase(new[] { "test", "test" }, ", ", "test, test")]
        public void JoinToString_Test(IEnumerable<string> input, string separator, string output)
        {
            Assert.That(input.JoinToString(separator), Is.EqualTo(output));
        }
    }
}