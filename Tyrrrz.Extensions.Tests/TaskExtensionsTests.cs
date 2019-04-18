using System.Threading.Tasks;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class TaskExtensionsTests
    {
        [Test]
        public async Task ParallelSelectAsync_Test()
        {
            // Arrange
            var array = new[] {0, 1, 2, 3, 4};

            // Act
            var selected = await array.ParallelSelectAsync(i => Task.Run(() => i * 2));

            // Assert
            Assert.That(selected, Is.EqualTo(new[] {0, 2, 4, 6, 8}));
        }

        [Test]
        public async Task ParallelForEachAsync_Test()
        {
            // Arrange
            var array = new[] {0, 1, 2, 3, 4};

            // Act
            await array.ParallelForEachAsync(i => Task.Run(() => array[i] = i * 2));

            // Assert
            Assert.That(array, Is.EqualTo(new[] {0, 2, 4, 6, 8}));
        }
    }
}