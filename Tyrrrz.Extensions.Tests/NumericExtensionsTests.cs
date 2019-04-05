using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class NumericExtensionsTests
    {
        [Test]
        [TestCase(5, 0, 10, 5)]
        [TestCase(5, 5, 10, 5)]
        [TestCase(5, 0, 5, 5)]
        [TestCase(5, 0, 3, 3)]
        [TestCase(5, 8, 10, 8)]
        public void Clamp_Test(int input, int min, int max, int output)
        {
            Assert.That(input.Clamp(min, max), Is.EqualTo(output));
        }

        [Test]
        [TestCase(5, 0, 5)]
        [TestCase(5, 5, 5)]
        [TestCase(5, 8, 8)]
        public void ClampMin_Test(int input, int min, int output)
        {
            Assert.That(input.ClampMin(min), Is.EqualTo(output));
        }

        [Test]
        [TestCase(5, 10, 5)]
        [TestCase(5, 5, 5)]
        [TestCase(5, 3, 3)]
        public void ClampMax_Test(int input, int max, int output)
        {
            Assert.That(input.ClampMax(max), Is.EqualTo(output));
        }
    }
}