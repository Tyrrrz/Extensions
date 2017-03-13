using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class ThreadingTests
    {
        [TestMethod]
        public async Task ParallelForEachAsyncTest()
        {
            var a = new[] {1, 5, 9, -5, 11};

            int aSum1 = 0;
            int aSum2 = 0;

            await a.ParallelForEachAsync(i =>
            {
                aSum1 += i;
            });
            await a.ParallelForEachAsync(async i =>
            {
                await Task.Yield();
                aSum2 += i;
            });

            Assert.AreEqual(21, aSum1);
            Assert.AreEqual(21, aSum2);
        }
    }
}