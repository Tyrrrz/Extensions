using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        [TestCase(null, true)]
        [TestCase(new object[0], true)]
        [TestCase(new object[] {1, 2, 3}, false)]
        public void IsNullOrEmpty_Test(IEnumerable<object> source, bool expectedOutput)
        {
            Assert.That(source.IsNullOrEmpty(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(null, new object[0])]
        [TestCase(new object[0], new object[0])]
        [TestCase(new object[] {1, 2, 3}, new object[] {1, 2, 3})]
        public void EmptyIfNull_Test(IEnumerable<object> source, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.EmptyIfNull(), Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GetSequenceHashCode_Test()
        {
            // Arrange
            var first = new[] {1, 2, 3};
            var second = new[] {3, 2, 1};
            var third = new[] {1, 2, 3};

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
        public void Random_Test()
        {
            // Arrange
            var input = new[] {1, 2, 3};

            // Act
            var random = input.Random();

            // Assert
            Assert.That(input, Has.Some.EqualTo(random));
        }

        [Test]
        public void RandomOrDefault_Test()
        {
            // Arrange
            var input1 = new[] {1, 2, 3};
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
            var input = new[] {100, 123, 150, 141, 193};

            // Act
            var distinct = input.Distinct(i => i % 10);

            // Assert
            Assert.That(distinct, Is.EqualTo(new[] {100, 123, 141}));
        }

        [Test]
        [TestCase(new object[] {1, 1, 2, 2, 3, 3}, 2, new object[] {1, 1, 3, 3})]
        [TestCase(new object[] {1, 2, 3}, 5, new object[] {1, 2, 3})]
        public void Except_Test(IEnumerable<object> source, object except, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.Except(except), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {null, 1, null, 2}, new object[] {1, 2})]
        [TestCase(new object[] {1, 2, 3}, new object[] {1, 2, 3})]
        public void ExceptDefault_Test(IEnumerable<object> source, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.ExceptDefault(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 1, 2, new object[] {2, 3})]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 0, 5, new object[] {1, 2, 3, 4, 5})]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 2, 0, new object[0])]
        public void Slice_Test(IEnumerable<object> source, int startAt, int count, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.Slice(startAt, count), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 3, new object[] {3, 4, 5})]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 5, new object[] {1, 2, 3, 4, 5})]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 0, new object[0])]
        public void TakeLast_Test(IEnumerable<object> source, int count, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.TakeLast(count), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 3, new object[] {1, 2})]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 5, new object[0])]
        [TestCase(new object[] {1, 2, 3, 4, 5}, 0, new object[] {1, 2, 3, 4, 5})]
        public void SkipLast_Test(IEnumerable<object> source, int count, IEnumerable<object> expectedOutput)
        {
            Assert.That(source.SkipLast(count), Is.EqualTo(expectedOutput));
        }

        [Test]
        public void TakeLastWhile_Test()
        {
            // Arrange
            var input = new[] {6, 2, 10, 4, 5};

            // Act
            var output = input.TakeLastWhile(i => i < 10);

            // Assert
            Assert.That(output, Is.EqualTo(new[] {4, 5}));
        }

        [Test]
        public void SkipLastWhile_Test()
        {
            // Arrange
            var input = new[] {6, 2, 10, 4, 5};

            // Act
            var output = input.SkipLastWhile(i => i < 10);

            // Assert
            Assert.That(output, Is.EqualTo(new[] {6, 2, 10}));
        }

        [Test]
        [TestCase(new object[] {6, 2, 10, 4, 5, 10}, 10, 2)]
        [TestCase(new object[] {6, 2, 10, 4, 5}, 20, -1)]
        public void IndexOf_Test(IEnumerable<object> source, object element, int expectedOutput)
        {
            Assert.That(source.IndexOf(element), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase(new object[] {6, 2, 10, 4, 5, 10}, 10, 5)]
        [TestCase(new object[] {6, 2, 10, 4, 5}, 20, -1)]
        public void LastIndexOf_Test(IEnumerable<object> source, object element, int expectedOutput)
        {
            Assert.That(source.LastIndexOf(element), Is.EqualTo(expectedOutput));
        }

        [Test]
        public void GroupContiguous_Test()
        {
            // Arrange
            var input = new[] {1, 2, 3, 4, 5, 6, 7, 8};

            // Act
            var groups = input.GroupContiguous(b => b.Count < 3);

            // Assert
            Assert.That(groups, Is.EqualTo(new[] {new[] {1, 2, 3}, new[] {4, 5, 6}, new[] {7, 8}}));
        }
    }
}