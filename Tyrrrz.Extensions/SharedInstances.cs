using System;

namespace Tyrrrz.Extensions
{
    internal static class SharedInstances
    {
        [ThreadStatic]
        private static Random _threadSafeRandom;

        public static Random Random => _threadSafeRandom ?? (_threadSafeRandom = new Random());
    }
}