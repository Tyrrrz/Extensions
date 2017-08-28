using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Extensions;

namespace Tests
{
    [TestClass]
    public class ThreadingTests
    {
        [TestMethod]
        public async Task ParallelForEachAsyncTest()
        {
            var a = new[] {1, 5, 9, -5, 11};

            int aSum = 0;

            await a.ParallelForEachAsync(i =>
            {
                aSum += i;
            });

            Assert.AreEqual(21, aSum);
        }

        [TestMethod]
        public async Task ParallelSelectAsyncTest()
        {
            var a = new[] {1, 5, 9, -5, 11};

            var aStr = (await a.ParallelSelectAsync(i => i.ToString())).ToArray();

            CollectionAssert.AreEquivalent(new[] {"1", "5", "9", "-5", "11"}, aStr);
        }
    }
}