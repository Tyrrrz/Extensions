using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class TimeSpanTests
    {
        [TestMethod]
        public void MultiplyTest()
        {
            var t1 = TimeSpan.FromMinutes(123);
            var t2 = t1.Multiply(2);

            Assert.AreEqual(t1.TotalSeconds * 2, t2.TotalSeconds, 10e-5);
        }

        [TestMethod]
        public void DivideTest()
        {
            var t1 = TimeSpan.FromMinutes(123);
            var t2 = t1.Divide(2);

            Assert.AreEqual(t1.TotalSeconds / 2, t2.TotalSeconds, 10e-5);
        }
    }
}