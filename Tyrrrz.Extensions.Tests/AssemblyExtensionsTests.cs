using System;
using System.Reflection;
using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class AssemblyExtensionsTests
    {
        [Test]
        public void GetManifestResourceString_Test()
        {
            // Arrange
            var rootNamespace = typeof(AssemblyExtensionsTests).Namespace;
            var resourceName = $"{rootNamespace}.TestData.TestManifestResource.txt";

            // Act
            var str = Assembly.GetExecutingAssembly().GetManifestResourceString(resourceName);

            // Assert
            Assert.That(str, Is.EqualTo("Hello world"));
        }
    }
}