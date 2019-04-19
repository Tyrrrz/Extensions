using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class SetExtensions
    {
        [Test]
        [TestCase(new object[] {1, 2, 3, 4, 5, 6}, new object[] {1, 2, 3, 4, 5, 6})]
        [TestCase(new object[] {1, 1, 2, 2, 3, 3}, new object[] {1, 2, 3})]
        public void ToHashSet_Test(IEnumerable<object> source, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.ToHashSet(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {1, 2, 3}, new object[] {4, 5, 6}, new object[] {1, 2, 3, 4, 5, 6})]
        [TestCase(new object[] {1, 2, 3}, new object[] {2, 2, 6}, new object[] {1, 2, 3, 6})]
        public void AddRange_Test(IEnumerable<object> source, IEnumerable<object> items, IEnumerable<object> expectedOutput)
        {
            // Arrange
            var set = source.ToHashSet();
            var initialCount = set.Count;

            // Act
            var delta = set.AddRange(items);

            // Assert
            Assert.That(set, Is.EqualTo(expectedOutput));
            Assert.That(delta, Is.EqualTo(set.Count - initialCount));
        }
    }
}