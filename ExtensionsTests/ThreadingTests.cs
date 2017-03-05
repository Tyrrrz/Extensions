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
            int t1 = 0;
            int t2 = 0;

            await a.ParallelForEachAsync(i =>
            {
                t1 += i;
            });
            await a.ParallelForEachAsync(async i =>
            {
                await Task.Yield();
                t2 += i;
            });

            Assert.AreEqual(21, t1);
            Assert.AreEqual(21, t2);
        }
    }
}