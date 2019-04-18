using NUnit.Framework;
using Tyrrrz.Extensions.Tests.TestData;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class EnumExtensionsTests
    {
        [Test]
        [TestCase("Two", TestEnum.Two)]
        [TestCase("tWo", TestEnum.Two, true)]
        public void ParseEnum_Test(string input, TestEnum output, bool ignoreCase = true)
        {
            Assert.That(input.ParseEnum<TestEnum>(ignoreCase), Is.EqualTo(output));
        }

        [Test]
        [TestCase("Two", TestEnum.Two)]
        [TestCase("tWo", TestEnum.Two, true)]
        [TestCase("Four", default(TestEnum))]
        [TestCase("", default(TestEnum))]
        [TestCase(null, default(TestEnum))]
        public void ParseEnumOrDefault_Test(string input, TestEnum output, bool ignoreCase = true)
        {
            Assert.That(input.ParseEnumOrDefault<TestEnum>(ignoreCase), Is.EqualTo(output));
        }
    }
}