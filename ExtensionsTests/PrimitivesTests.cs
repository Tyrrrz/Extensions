using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class PrimitivesTests
    {
        [TestMethod]
        public void ConvertToTest()
        {
            var a = (object) "test";
            var b = 1;

            var aConvertTo = a.ConvertTo<string>();
            var bConvertTo = b.ConvertTo<bool>();

            Assert.AreEqual("test", aConvertTo);
            Assert.AreEqual(true, bConvertTo);
        }

        [TestMethod]
        public void ConvertToOrDefaultTest()
        {
            var a = 1;

            var aConvertToOrDefault = a.ConvertToOrDefault<bool>();
            var aConvertToOrDefaultFail = a.ConvertToOrDefault<DateTime>();

            Assert.AreEqual(true, aConvertToOrDefault);
            Assert.AreEqual(default(DateTime), aConvertToOrDefaultFail);
        }

        [TestMethod]
        public void IsEitherTest()
        {
            Assert.IsTrue(5.IsEither(1, 2, 3, 4, 5, 6));
            Assert.IsFalse(131.IsEither(1, 2, 3, 4, 5, 6));

            var x = new[] {1, 12, 14, 19};
            Assert.IsTrue(14.IsEither(x));
        }

        [TestMethod]
        public void IsInRangeTest()
        {
            Assert.IsTrue(5.IsInRange(0, 10));
            Assert.IsTrue(5.IsInRange(0, 5));
            Assert.IsTrue(5.IsInRange(5, 10));
            Assert.IsFalse(5.IsInRange(6, 11));
            Assert.IsFalse(5.IsInRange(0, 3));
        }

        [TestMethod]
        public void ClampTest()
        {
            Assert.IsTrue(10.Clamp(0, 5) == 5);
            Assert.IsTrue(10.Clamp(0, 10) == 10);
            Assert.IsTrue(10.Clamp(10, 10) == 10);
            Assert.IsTrue(10.Clamp(15, 16) == 15);
        }

        [TestMethod]
        public void ClampMinTest()
        {
            Assert.IsTrue(10.ClampMin(0) == 10);
            Assert.IsTrue(10.ClampMin(11) == 11);
        }

        [TestMethod]
        public void ClampMaxTest()
        {
            Assert.IsTrue(10.ClampMax(11) == 10);
            Assert.IsTrue(10.ClampMax(0) == 0);
        }

        [TestMethod]
        public void CeilingToIntTest()
        {
            Assert.AreEqual(5, 4.15.CeilingToInt());
            Assert.AreEqual(5, 4.7.CeilingToInt());
            Assert.AreEqual(5, 5.0.CeilingToInt());
        }

        [TestMethod]
        public void FloorToIntTest()
        {
            Assert.AreEqual(4, 4.15.FloorToInt());
            Assert.AreEqual(4, 4.7.FloorToInt());
            Assert.AreEqual(4, 4.0.FloorToInt());
        }

        [TestMethod]
        public void FractionTest()
        {
            Assert.AreEqual(0.15, 4.15.Fraction(), 10e-10);
            Assert.AreEqual(0.7, 4.7.Fraction(), 10e-10);
            Assert.AreEqual(0.0, 4.0.Fraction(), 10e-10);
        }
    }
}