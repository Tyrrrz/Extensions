using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tyrrrz.Extensions;
using Tyrrrz.Extensions.Types;

namespace Tests
{
    [TestClass]
    public class CollectionsTests
    {
        [TestMethod]
        public void NotNullAndAnyTest()
        {
            var a = new[] {1, 2, 3, 4, 5};
            var b = new int[0];
            int[] c = null;

            Assert.IsTrue(a.NotNullAndAny());
            Assert.IsFalse(b.NotNullAndAny());
            Assert.IsFalse(c.NotNullAndAny());

            Assert.IsTrue(a.NotNullAndAny(i => i == 5));
            Assert.IsFalse(a.NotNullAndAny(i => i == 6));
            Assert.IsFalse(b.NotNullAndAny(i => i == 6));
            Assert.IsFalse(c.NotNullAndAny(i => i == 6));
        }

        [TestMethod]
        public void GetRandomTest()
        {
            var a = new[] {1, 2, 3, 4, 5};

            var random = a.GetRandom();

            CollectionAssert.Contains(a, random);
        }

        [TestMethod]
        public void GetRandomOrDefaultTest()
        {
            var a = new[] {1, 2, 3, 4, 5};
            var b = new int[0];

            var aRandom = a.GetRandomOrDefault();
            var bRandom = b.GetRandomOrDefault();

            CollectionAssert.Contains(a, aRandom);
            Assert.AreEqual(0, bRandom);
        }

        [TestMethod]
        public void EmptyIfNullTest()
        {
            var a = new[] {1, 2, 3, 4, 5};
            var b = new int[0];
            int[] c = null;

            var aEmptyIfNull = a.EmptyIfNull().ToArray();
            var bEmptyIfNull = b.EmptyIfNull().ToArray();
            // ReSharper disable once ExpressionIsAlwaysNull
            var cEmptyIfNull = c.EmptyIfNull().ToArray();

            CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5}, aEmptyIfNull);
            CollectionAssert.AreEqual(new int[0], bEmptyIfNull);
            CollectionAssert.AreEqual(new int[0], cEmptyIfNull);
        }

        [TestMethod]
        public void DistinctTest()
        {
            var a = new[] {"qqq", "ww", "zz", "qqq", "11111"};

            var aDistinct = a.Distinct(i => i.Length).ToArray();

            CollectionAssert.AreEqual(new[] {"qqq", "ww", "11111"}, aDistinct);
        }

        [TestMethod]
        public void SequenceHashCodeTest()
        {
            var a = new[] {"asd", "qwe", "xxx"};
            var b = new[] {"xxx", "qwe", "asd"};
            var c = new[] {"ddd"};

            int aHash = a.SequenceHashCode();
            int aHashIgnoreOrder = a.SequenceHashCode(true);
            int bHash = b.SequenceHashCode();
            int bHashIgnoreOrder = b.SequenceHashCode(true);
            int cHash = c.SequenceHashCode();
            int cHashIgnoreOrder = c.SequenceHashCode(true);

            Assert.AreNotEqual(aHash, bHash);
            Assert.AreNotEqual(aHash, cHash);
            Assert.AreNotEqual(bHash, cHash);
            Assert.AreEqual(aHashIgnoreOrder, bHashIgnoreOrder);
            Assert.AreNotEqual(aHashIgnoreOrder, cHashIgnoreOrder);
            Assert.AreNotEqual(bHashIgnoreOrder, cHashIgnoreOrder);
            Assert.AreNotEqual(aHash, cHashIgnoreOrder);
            Assert.AreNotEqual(bHash, cHashIgnoreOrder);
            Assert.AreEqual(cHash, cHashIgnoreOrder);
        }

        [TestMethod]
        public void ExceptTest()
        {
            var a = new[] {"asd", "qwe", "dfg"};

            var aExcept = a.Except("qwe").ToArray();
            var aExceptNonExisting = a.Except("zzz").ToArray();

            CollectionAssert.AreEqual(new[] {"asd", "dfg"}, aExcept);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg"}, aExceptNonExisting);
        }

        [TestMethod]
        public void ExceptDefaultTest()
        {
            var a = new[] {0, 1, 2, 3, 4, 5};
            var b = new[] {"asd", "qwe", null};

            var aExceptDefault = a.ExceptDefault().ToArray();
            var bExceptDefault = b.ExceptDefault().ToArray();

            CollectionAssert.AreEqual(new[] {1, 2, 3, 4, 5}, aExceptDefault);
            CollectionAssert.AreEqual(new[] {"asd", "qwe"}, bExceptDefault);
        }

        [TestMethod]
        public void TakeLastTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "zzz"};

            var aTakeLast = a.TakeLast(2).ToArray();
            var aTakeLastTooMany = a.TakeLast(100).ToArray();
            var aTakeLastNone = a.TakeLast(0).ToArray();

            CollectionAssert.AreEqual(new[] {"dfg", "zzz"}, aTakeLast);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "zzz"}, aTakeLastTooMany);
            CollectionAssert.AreEqual(new string[0], aTakeLastNone);
        }

        [TestMethod]
        public void SkipLastTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "zzz"};

            var aSkipLast = a.SkipLast(2).ToArray();
            var aSkipLastTooMany = a.SkipLast(100).ToArray();
            var aSkipLastNone = a.SkipLast(0).ToArray();

            CollectionAssert.AreEqual(new[] {"asd", "qwe"}, aSkipLast);
            CollectionAssert.AreEqual(new string[0], aSkipLastTooMany);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "zzz"}, aSkipLastNone);
        }

        [TestMethod]
        public void TakeLastWhileTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "zzz"};

            var aTakeLastWhile = a.TakeLastWhile(s => s != "qwe").ToArray();
            var aTakeLastWhileTooMany = a.TakeLastWhile(s => s != "xxx").ToArray();
            var aTakeLastWhileNone = a.TakeLastWhile(s => s != "zzz").ToArray();

            CollectionAssert.AreEqual(new[] {"dfg", "zzz"}, aTakeLastWhile);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "zzz"}, aTakeLastWhileTooMany);
            CollectionAssert.AreEqual(new string[0], aTakeLastWhileNone);
        }

        [TestMethod]
        public void SkipLastWhileTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "zzz"};

            var aSkipLastWhile = a.SkipLastWhile(s => s != "qwe").ToArray();
            var aSkipLastWhileTooMany = a.SkipLastWhile(s => s != "xxx").ToArray();
            var aSkipLastWhileNone = a.SkipLastWhile(s => s != "zzz").ToArray();

            CollectionAssert.AreEqual(new[] {"asd", "qwe"}, aSkipLastWhile);
            CollectionAssert.AreEqual(new string[0], aSkipLastWhileTooMany);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "zzz"}, aSkipLastWhileNone);
        }

        [TestMethod]
        public void ForEachTest()
        {
            var a = new[] {"asd", "qwe", "dfg"};
            int i = 0;

            a.ForEach(s => Assert.AreEqual(s, a[i++]));
            Assert.AreEqual(3, i);
        }

        [TestMethod]
        public void ToHashSetTest()
        {
            var a = new[] {"asd", "q", "we", "ASD", "WE", "we"};

            var aHashSet = a.ToHashSet().ToArray();

            CollectionAssert.AreEqual(new[] {"asd", "q", "we", "ASD", "WE"}, aHashSet);
        }

        [TestMethod]
        public void AddIfDistinctTest()
        {
            var a = new List<string> {"asd", "qwe", "dfg", "DFG"};

            bool aAddIfDistinct = a.AddIfDistinct("xxx");
            bool aAddIfDistinctExists = a.AddIfDistinct("asd");

            Assert.IsTrue(aAddIfDistinct);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "DFG", "xxx"}, a);
            Assert.IsFalse(aAddIfDistinctExists);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg", "DFG", "xxx"}, a);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "ASD", "QWE", "qwe", "ASD"};

            int aIndexOf = a.IndexOf("qwe");
            int aIndexOfNonExisting = a.IndexOf("qqq");
            int aIndexOfPredicate = a.IndexOf(s => s == "ASD");

            Assert.AreEqual(1, aIndexOf);
            Assert.AreEqual(-1, aIndexOfNonExisting);
            Assert.AreEqual(3, aIndexOfPredicate);
        }

        [TestMethod]
        public void LastIndexOfTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "ASD", "QWE", "qwe", "ASD"};

            int aLastIndexOf = a.LastIndexOf("qwe");
            int aLastIndexOfNonExisting = a.LastIndexOf("qqq");
            int aLastIndexOfPredicate = a.LastIndexOf(s => s == "ASD");

            Assert.AreEqual(5, aLastIndexOf);
            Assert.AreEqual(-1, aLastIndexOfNonExisting);
            Assert.AreEqual(6, aLastIndexOfPredicate);
        }

        [TestMethod]
        public void LastIndexTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "ASD", "QWE"};
            var b = new[,] {{"asd", "qwe", "qqq"}, {"dfg", "zzz", "www"}};

            int aLastIndex = a.LastIndex();
            int bLastIndex = b.LastIndex();
            int bLastIndexDimension = b.LastIndex(1);
            int bLastIndexDimensionNonExisting = b.LastIndex(1337);

            Assert.AreEqual(4, aLastIndex);
            Assert.AreEqual(1, bLastIndex);
            Assert.AreEqual(2, bLastIndexDimension);
            Assert.AreEqual(-1, bLastIndexDimensionNonExisting);
        }

        [TestMethod]
        public void GetOrDefaultTest()
        {
            var a = new Dictionary<string, string>
            {
                {"asd", "qwe"},
                {"zxc", "bnm"}
            };

            string aGetOrDefault = a.GetOrDefault("zxc");
            string aGetOrDefaultNonExisting = a.GetOrDefault("qoeo");

            Assert.AreEqual("bnm", aGetOrDefault);
            Assert.AreEqual(null, aGetOrDefaultNonExisting);
        }

        [TestMethod]
        public void FillTest()
        {
            var a = new[] {"asd", "qwe", "dfg", "ASD", "QWE"};
            var b = new[] {"asd", "qwe", "dfg", "ASD", "QWE"};
            var c = new[] {"asd", "qwe", "dfg", "ASD", "QWE"};

            a.Fill("xxx");
            b.Fill("xxx", 2);
            c.Fill("xxx", 1, 2);

            CollectionAssert.AreEqual(new[] {"xxx", "xxx", "xxx", "xxx", "xxx"}, a);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "xxx", "xxx", "xxx"}, b);
            CollectionAssert.AreEqual(new[] {"asd", "xxx", "xxx", "ASD", "QWE"}, c);
        }

        [TestMethod]
        public void EnsureMaxCountTest()
        {
            var a = new List<string> {"asd", "qwe", "dfg", "DFG"};
            var b = new List<string> {"asd", "qwe", "dfg", "DFG"};
            var c = new List<string> {"asd", "qwe", "dfg", "DFG"};
            var d = new List<string> {"asd", "qwe", "dfg", "DFG"};

            a.EnsureMaxCount(3);
            b.EnsureMaxCount(3, EnsureMaxCountMode.DeleteLast);
            c.EnsureMaxCount(3, EnsureMaxCountMode.DeleteRandom);
            d.EnsureMaxCount(3, EnsureMaxCountMode.DeleteAll);

            CollectionAssert.AreEqual(new[] {"qwe", "dfg", "DFG"}, a);
            CollectionAssert.AreEqual(new[] {"asd", "qwe", "dfg"}, b);
            Assert.AreEqual(3, c.Count);
            CollectionAssert.AreEqual(new string[0], d);
        }

        [TestMethod]
        public void SetOrAddTest()
        {
            var d = new Dictionary<string, string>
            {
                {"asd", "qwe"},
                {"zxc", "bnm"}
            };

            bool dSetOrAddExisting = d.SetOrAdd("zxc", "ooo");
            Assert.IsTrue(dSetOrAddExisting);
            CollectionAssert.AreEqual(new Dictionary<string, string> {{"asd", "qwe"}, {"zxc", "ooo"}}, d);

            bool dSetOrAddNonExisting = d.SetOrAdd("123", "456");
            Assert.IsFalse(dSetOrAddNonExisting);
            CollectionAssert.AreEqual(new Dictionary<string, string> {{"asd", "qwe"}, {"zxc", "ooo"}, {"123", "456"}},
                d);
        }
    }
}