using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class EncodingExtensionsTests
    {
        [Test]
        [TestCase("test")]
        [TestCase("")]
        public void GetBytes_GetString_Test(string input)
        {
            // Act
            var bytes = input.GetBytes();
            var backToString = bytes.GetString();

            // Assert
            Assert.That(backToString, Is.EqualTo(input));
        }
    }
}