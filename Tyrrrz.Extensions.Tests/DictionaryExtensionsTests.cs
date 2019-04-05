using System.Collections.Generic;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class DictionaryExtensionsTests
    {
        [Test]
        public void GetValueOrDefault_Test()
        {
            // Arrange
            var dic = new Dictionary<string, string>
            {
                {"test", "1"},
                {"aaa", "bbb"}
            };

            // Act
            var existingValue = dic.GetValueOrDefault("test");
            var nonExistingValue = dic.GetValueOrDefault("x");

            // Assert
            Assert.That(existingValue, Is.EqualTo("1"));
            Assert.That(nonExistingValue, Is.EqualTo(default(string)));
        }
    }
}