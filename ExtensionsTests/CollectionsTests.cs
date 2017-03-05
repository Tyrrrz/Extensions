using Tyrrrz.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tyrrrz.Extensions.Tests
{
    [TestClass]
    public class CollectionsTests
    {
        [TestMethod]
        public void NotNullAndAnyTest()
        {
            var a = new[] { 1, 2, 3, 4, 5 };
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
            var a = new[] { 1, 2, 3, 4, 5 };
            var r = a.GetRandom();

            Assert.IsTrue(a.Contains(r));
        }

        [TestMethod]
        public void GetRandomOrDefaultTest()
        {
            var a = new[] { 1, 2, 3, 4, 5 };
            var b = new int[0];

            var ar = a.GetRandomOrDefault();
            var br = b.GetRandomOrDefault();

            Assert.IsTrue(a.Contains(ar));
            Assert.AreEqual(0, br);
        }

        [TestMethod]
        public void WithTest()
        {
            var a = new[] { "asd", "qwe", "dfg" };

            var ws = a.With("zzz").ToArray();
            var wsm = a.With("123", "456").ToArray();
            var wse = a.With(new[] {"kkk", "lll"}, new[] {"ppp"}).ToArray();

            Assert.AreEqual(4, ws.Length);
            Assert.IsTrue(ws.Contains("zzz"));
            Assert.AreEqual(5, wsm.Length);
            Assert.IsTrue(wsm.Contains("123"));
            Assert.IsTrue(wsm.Contains("456"));
            Assert.AreEqual(6, wse.Length);
            Assert.IsTrue(wse.Contains("kkk"));
            Assert.IsTrue(wse.Contains("lll"));
            Assert.IsTrue(wse.Contains("ppp"));
        }

        [TestMethod]
        public void WithoutTest()
        {
            var a = new[] { "asd", "qwe", "dfg" };

            var wa = a.Without("qwe").ToArray();
            var wea = a.Without("qWE", StringComparer.OrdinalIgnoreCase).ToArray();

            Assert.AreEqual(2, wa.Length);
            Assert.IsFalse(wa.Contains("qwe"));

            Assert.AreEqual(2, wea.Length);
            Assert.IsFalse(wea.Contains("qwe"));
        }

        [TestMethod]
        public void WithoutNullTest()
        {
            var a = new[] { "asd", "qwe", "dfg", null, "null" };

            var wna = a.WithoutNull().ToArray();

            Assert.AreEqual(4, wna.Length);
            Assert.IsFalse(wna.Contains(null));
        }

        [TestMethod]
        public void WithoutDefaultTest()
        {
            var a = new[] { 0, 1, 2, 3, 4, 5 };

            var wda = a.WithoutDefault().ToArray();

            Assert.AreEqual(5, wda.Length);
            Assert.IsFalse(wda.Contains(0));
        }

        [TestMethod]
        public void TakeLastTest()
        {
            var a = new[] { "asd", "qwe", "dfg" };

            var tla = a.TakeLast(2).ToArray();
            var tlma = a.TakeLast(100).ToArray();
            var tlla = a.TakeLast(0).ToArray();

            Assert.AreEqual(2, tla.Length);
            Assert.IsFalse(tla.Contains("asd"));
            Assert.AreEqual(a.Length, tlma.Length);
            Assert.AreEqual(0, tlla.Length);
        }

        [TestMethod]
        public void SkipLastTest()
        {
            var a = new[] { "asd", "qwe", "dfg" };

            var sla = a.SkipLast(2).ToArray();
            var slma = a.SkipLast(100).ToArray();
            var slla = a.SkipLast(0).ToArray();

            Assert.AreEqual(1, sla.Length);
            Assert.IsFalse(sla.Contains("qwe"));
            Assert.IsFalse(sla.Contains("dfg"));
            Assert.AreEqual(0, slma.Length);
            Assert.AreEqual(a.Length, slla.Length);
        }

        [TestMethod]
        public void ForEachTest()
        {
            var a = new[] { "asd", "qwe", "dfg" };
            int i = 0;

            a.ForEach(s => Assert.AreEqual(s, a[i++]));
        }

        [TestMethod]
        public void ToHashSetTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE", "qwe" };

            var hs = a.ToHashSet();
            var hse = a.ToHashSet(StringComparer.OrdinalIgnoreCase);

            Assert.AreEqual(5, hs.Count);
            Assert.AreEqual(3, hse.Count);
        }

        [TestMethod]
        public void AddIfDistinctTest()
        {
            var a = new List<string> { "asd", "qwe", "dfg", "DFG" };

            bool aida = a.AddIfDistinct("xxx");
            bool aidn = a.AddIfDistinct("asd");
            bool aidne = a.AddIfDistinct("DFG", StringComparer.OrdinalIgnoreCase);

            Assert.IsTrue(aida);
            Assert.IsFalse(aidn);
            Assert.IsFalse(aidne);
            Assert.AreEqual(5, a.Count);
        }

        [TestMethod]
        public void IndexOfTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE" };

            int io = a.IndexOf("QWE");
            int ion = a.IndexOf("qqq");
            int ioe = a.IndexOf("QWE", StringComparer.OrdinalIgnoreCase);
            int iop = a.IndexOf(s => s == "ASD");

            Assert.AreEqual(4, io);
            Assert.AreEqual(-1, ion);
            Assert.AreEqual(1, ioe);
            Assert.AreEqual(3, iop);
        }

        [TestMethod]
        public void LastIndexOfTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE" };

            int lio = a.LastIndexOf("qwe");
            int lion = a.LastIndexOf("qqq");
            int lioe = a.LastIndexOf("qwe", StringComparer.OrdinalIgnoreCase);
            int liop = a.LastIndexOf(s => s == "ASD");

            Assert.AreEqual(1, lio);
            Assert.AreEqual(-1, lion);
            Assert.AreEqual(4, lioe);
            Assert.AreEqual(3, liop);
        }

        [TestMethod]
        public void LastIndexTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE" };

            int li = a.LastIndex();

            Assert.AreEqual(4, li);
        }

        [TestMethod]
        public void GetOrDefaultTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE" };
            var d = new Dictionary<string, string>
            {
                {"asd", "qwe"},
                {"zxc", "bnm"}
            };

            string x = a.GetOrDefault(1);
            string xn = a.GetOrDefault(15);
            string xd = d.GetOrDefault("zxc");
            string xdn = d.GetOrDefault("qoeo");

            Assert.AreEqual("qwe", x);
            Assert.AreEqual(null, xn);
            Assert.AreEqual("bnm", xd);
            Assert.AreEqual(null, xdn);
        }

        [TestMethod]
        public void FillTest()
        {
            var a = new[] { "asd", "qwe", "dfg", "ASD", "QWE" };

            a.Fill("xxx");
            Assert.AreEqual(5, a.Length);
            Assert.IsTrue(a.All(s => s == "xxx"));
        }

        [TestMethod]
        public void EnsureMaxCountTest()
        {
            var a = new List<string> { "asd", "qwe", "dfg", "DFG" };

            a.EnsureMaxCount(3);
            Assert.AreEqual(3, a.Count);
        }

        [TestMethod]
        public void SetOrAddTest()
        {
            var d = new Dictionary<string, string>
            {
                {"asd", "qwe"},
                {"zxc", "bnm"}
            };

            bool s = d.SetOrAdd("zxc", "ooo");
            bool a = d.SetOrAdd("123", "456");

            Assert.IsTrue(s);
            Assert.IsFalse(a);
            Assert.AreEqual(3, d.Count);
            Assert.AreEqual("ooo", d["zxc"]);
            Assert.AreEqual("456", d["123"]);
        }
    }
}