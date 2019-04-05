using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        [TestCase(new object[] { 1, 2, 3 }, new object[] { 1, 2, 3 })]
        [TestCase(new object[0], new object[0])]
        [TestCase(null, new object[0])]
        public void EmptyIfNull_Test(IEnumerable<object> input, IEnumerable<object> output)
        {
            Assert.That(input.EmptyIfNull(), Is.EquivalentTo(output));
        }

        [Test]
        public void GetSequenceHashCode_Test()
        {
            // Arrange
            var first = new[] { 1, 2, 3 };
            var second = new[] { 3, 2, 1 };
            var third = new[] { 1, 2, 3 };

            // Act
            var firstHashCode = first.GetSequenceHashCode();
            var secondHashCode = second.GetSequenceHashCode();
            var thirdHashCode = third.GetSequenceHashCode();
            var firstHashCodeIgnoreOrder = first.GetSequenceHashCode(true);
            var secondHashCodeIgnoreOrder = second.GetSequenceHashCode(true);
            var thirdHashCodeIgnoreOrder = third.GetSequenceHashCode(true);

            // Assert
            Assert.That(firstHashCode, Is.Not.EqualTo(secondHashCode));
            Assert.That(firstHashCode, Is.EqualTo(thirdHashCode));
            Assert.That(secondHashCode, Is.Not.EqualTo(thirdHashCode));

            Assert.That(firstHashCodeIgnoreOrder, Is.EqualTo(secondHashCodeIgnoreOrder));
            Assert.That(firstHashCodeIgnoreOrder, Is.EqualTo(thirdHashCodeIgnoreOrder));
            Assert.That(secondHashCodeIgnoreOrder, Is.EqualTo(thirdHashCodeIgnoreOrder));
        }

        [Test]
        public void ToHashSet_Test()
        {
            // Arrange
            var input = new[] { 1, 1, 2, 2, 3, 3 };

            // Act
            var hashSet = input.ToHashSet();

            // Assert
            Assert.That(hashSet, Is.EquivalentTo(new[] { 1, 2, 3 }));
        }

        [Test]
        public void Random_Test()
        {
            // Arrange
            var input = new[] { 1, 2, 3 };

            // Act
            var random = input.Random();

            // Assert
            Assert.That(input, Has.Some.EqualTo(random));
        }

        [Test]
        public void RandomOrDefault_Test()
        {
            // Arrange
            var input1 = new[] { 1, 2, 3 };
            var input2 = new int[0];

            // Act
            var random1 = input1.RandomOrDefault();
            var random2 = input2.RandomOrDefault();

            // Assert
            Assert.That(input1, Has.Some.EqualTo(random1));
            Assert.That(random2, Is.EqualTo(default(int)));
        }

        [Test]
        public void Distinct_Test()
        {
            // Arrange
            var input = new[] { 1, 2, 3, 4, 5 };

            // Act
            var distinct = input.Distinct(i => i % 2 == 0);

            // Assert
            // TODO
            Assert.That(distinct, Is.EquivalentTo(new[] { 2, 4 }));
        }
    }
}