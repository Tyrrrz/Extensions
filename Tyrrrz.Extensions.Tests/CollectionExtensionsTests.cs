using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        public void AddRange_Test()
        {
            // Arrange
            var collection = new List<string>() as ICollection<string>;
            var items = new[]
            {
                "hello",
                "world"
            };

            // Act
            collection.AddRange(items);

            // Assert
            Assert.That(collection, Is.EqualTo(items));
        }
    }
}