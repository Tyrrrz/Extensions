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

        [Test]
        [TestCase(new byte[] {0, 13, 19, 22, 99})]
        [TestCase(new byte[0])]
        public void ToBase64_FromBase64_Test(byte[] input)
        {
            // Act
            var base64 = input.ToBase64();
            var backToBytes = base64.FromBase64();

            // Assert
            Assert.That(backToBytes, Is.EqualTo(input));
        }
    }
}