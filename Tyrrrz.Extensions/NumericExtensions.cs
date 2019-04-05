using System;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for numeric types.
    /// </summary>
    public static class NumericExtensions
    {
        /// <summary>
        /// Clamps a value to a given range.
        /// </summary>
        [Pure, NotNull]
        public static T Clamp<T>([NotNull] this T value, [NotNull] T min, [NotNull] T max) where T : IComparable<T>
        {
            value.GuardNotNull(nameof(value));
            min.GuardNotNull(nameof(min));
            max.GuardNotNull(nameof(max));

            // Ensure max is greater than min
            if (max.CompareTo(min) < 0)
                throw new ArgumentException("Max must be greater than or equal to min.");

            // If value is less than min - return min
            if (value.CompareTo(min) <= 0)
                return min;

            // If value is greater than max - return max
            if (value.CompareTo(max) >= 0)
                return max;

            // Otherwise - return value
            return value;
        }

        /// <summary>
        /// Clamps a value to a given range.
        /// </summary>
        [Pure, NotNull]
        public static T ClampMin<T>([NotNull] this T value, [NotNull] T min) where T : IComparable<T>
        {
            value.GuardNotNull(nameof(value));
            min.GuardNotNull(nameof(min));

            // If value is less than min - return min
            if (value.CompareTo(min) <= 0)
                return min;

            // Otherwise - return value
            return value;
        }

        /// <summary>
        /// Clamps a value to a given range.
        /// </summary>
        [Pure, NotNull]
        public static T ClampMax<T>([NotNull] this T value, [NotNull] T max) where T : IComparable<T>
        {
            value.GuardNotNull(nameof(value));
            max.GuardNotNull(nameof(max));

            // If value is greater than max - return max
            if (value.CompareTo(max) >= 0)
                return max;

            // Otherwise - return value
            return value;
        }
    }
}