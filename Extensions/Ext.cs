using System;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extension methods for rapid development
    /// </summary>
    public static partial class Ext
    {
        /// <summary>
        /// Generic string parse delegate
        /// </summary>
        public delegate T ParseDelegate<out T>(string str);

        /// <summary>
        /// Generic string try-parse delegate
        /// </summary>
        public delegate bool TryParseDelegate<T>(string str, out T result);

        [AssertionMethod]
        private static void GuardNull<T>(
            [NoEnumeration, AssertionCondition(AssertionConditionType.IS_NOT_NULL)] T value, string name,
            string message = null)
        {
            if (value != null)
                return;

            if (message == null)
                message = $"Parameter [{name}] cannot be null";

            throw new ArgumentNullException(name, message);
        }

        [AssertionMethod]
        private static void GuardRange<T>(T value, T min, T max, string name, string message = null)
            where T : IComparable
        {
            if (value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0)
                return;

            if (message == null)
                message = $"Parameter [{name}] needs to be inside range [{min} -> {max}]";

            throw new ArgumentOutOfRangeException(name, message);
        }

        [AssertionMethod]
        private static void GuardMin<T>(T value, T min, string name, string message = null) where T : IComparable
        {
            if (value.CompareTo(min) >= 0)
                return;

            if (message == null)
                message = $"Parameter [{name}] needs to be greater than or equal to [{min}]";

            throw new ArgumentOutOfRangeException(name, message);
        }

        [AssertionMethod]
        private static void GuardMax<T>(T value, T max, string name, string message = null) where T : IComparable
        {
            if (value.CompareTo(max) <= 0)
                return;

            if (message == null)
                message = $"Parameter [{name}] needs to be lesser than or equal to [{max}]";

            throw new ArgumentOutOfRangeException(name, message);
        }

        [AssertionMethod]
        private static void GuardCondition([AssertionCondition(AssertionConditionType.IS_FALSE)] bool condition,
            string name, string message = null)
        {
            if (!condition)
                return;

            if (message == null)
                message = $"Parameter [{name}] is invalid";

            throw new ArgumentException(message, name);
        }
    }
}