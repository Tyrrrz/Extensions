using NUnit.Framework;

namespace Tyrrrz.Extensions.Tests
{
    [TestFixture]
    public class UriExtensionsTests
    {
        [Test]
        [TestCase("https://test.com", "https://test.com/")]
        [TestCase("www.test.com", "http://www.test.com/")]
        public void ToUri_Test(string input, string expectedOutput)
        {
            Assert.That(input.ToUri().ToString(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase("route/resource", "test.com", "http://test.com/route/resource")]
        [TestCase("/root", "https://test.com/other", "https://test.com/root")]
        public void ToUri_Test(string input, string baseUri, string expectedOutput)
        {
            Assert.That(input.ToUri(baseUri).ToString(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase("http://test.com", "a", "b", "http://test.com/?a=b")]
        [TestCase("http://test.com/?a=b", "a", "x", "http://test.com/?a=x")]
        [TestCase("http://test.com/?x=y&c=d", "a", "b", "http://test.com/?x=y&c=d&a=b")]
        public void SetQueryParameter_Test(string input, string key, string value, string expectedOutput)
        {
            Assert.That(input.ToUri().SetQueryParameter(key, value).ToString(), Is.EqualTo(expectedOutput));
        }

        [Test]
        [TestCase("http://test.com", "a", "b", "http://test.com/a/b")]
        [TestCase("http://test.com/a/b", "a", "x", "http://test.com/a/x")]
        [TestCase("http://test.com/x/y/c/d", "a", "b", "http://test.com/x/y/c/d/a/b")]
        public void SetRouteParameter_Test(string input, string key, string value, string expectedOutput)
        {
            Assert.That(input.ToUri().SetRouteParameter(key, value).ToString(), Is.EqualTo(expectedOutput));
        }
    }
}