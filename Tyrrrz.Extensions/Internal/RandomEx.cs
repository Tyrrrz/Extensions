using System;

namespace Tyrrrz.Extensions.Internal
{
    internal static class RandomEx
    {
        private static readonly Random _random = new Random();

        public static int GetInt(int inclusiveMin = 0, int exclusiveMax = int.MaxValue)
        {
            lock (_random)
            {
                return _random.Next(inclusiveMin, exclusiveMax);
            }
        }

        public static double GetDouble()
        {
            lock (_random)
            {
                return _random.NextDouble();
            }
        }

        public static void FillBytes(byte[] output)
        {
            lock (_random)
            {
                _random.NextBytes(output);
            }
        }
    }
}