using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class MiscExtensionsTests
    {
        [Test]
        [TestCase(1, new object[] {1, 2, 3, 4, 5}, true)]
        [TestCase(0, new object[] {1, 2, 3, 4, 5}, false)]
        [TestCase(1, new object[0], false)]
        [TestCase(1, new object[] {0, 0, 0, 1, 1}, true)]
        public void IsEither_Test(object input, IEnumerable<object> variants, bool output)
        {
            Assert.That(input.IsEither(variants), Is.EqualTo(output));
        }

        [Test]
        public void IsEither_Test()
        {
            Assert.That(5.IsEither(1, 2, 3, 4, 5));
        }
    }
}