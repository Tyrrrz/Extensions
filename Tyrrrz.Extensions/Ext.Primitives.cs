using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Converts an object to another type and returns the converted instance.
        /// </summary>
        [Pure]
        public static T ConvertTo<T>(this object obj)
        {
            return (T) Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Tries to convert an object to another type, returns the converted instance if successful or default value if not.
        /// </summary>
        [Pure]
        public static T ConvertToOrDefault<T>(this object obj, T defaultValue = default(T))
        {
            try
            {
                return ConvertTo<T>(obj);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Determines whether the object is equal to either of elements in a sequence.
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> enumerable,
            [NotNull] IEqualityComparer<T> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return enumerable.Any(other => comparer.Equals(obj, other));
        }

        /// <summary>
        /// Determines whether the object is equal to either of elements in a sequence.
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> enumerable)
            => IsEither(obj, enumerable, EqualityComparer<T>.Default);

        /// <summary>
        /// Determines whether the object is equal to either of the parameters.
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, params T[] objs)
            => IsEither(obj, (IEnumerable<T>) objs);

        /// <summary>
        /// Determines whether the value is inside given range (inclusive).
        /// </summary>
        [Pure]
        public static bool IsInRange<T>([NotNull] this T value, [NotNull] T min, [NotNull] T max) where T : IComparable<T>
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (min == null)
                throw new ArgumentNullException(nameof(min));
            if (max == null)
                throw new ArgumentNullException(nameof(max));

            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        /// <summary>
        /// Returns closest value in the given range (inclusive).
        /// </summary>
        [Pure, NotNull]
        public static T Clamp<T>([NotNull] this T value, [NotNull] T min, [NotNull] T max) where T : IComparable<T>
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (min == null)
                throw new ArgumentNullException(nameof(min));
            if (max == null)
                throw new ArgumentNullException(nameof(max));

            return value.CompareTo(min) <= 0 ? min : value.CompareTo(max) >= 0 ? max : value;
        }

        /// <summary>
        /// Returns closest value above the boundary.
        /// </summary>
        [Pure, NotNull]
        public static T ClampMin<T>([NotNull] this T value, [NotNull] T min) where T : IComparable<T>
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (min == null)
                throw new ArgumentNullException(nameof(min));

            return value.CompareTo(min) <= 0 ? min : value;
        }

        /// <summary>
        /// Returns closest value below the boundary.
        /// </summary>
        [Pure, NotNull]
        public static T ClampMax<T>([NotNull] this T value, [NotNull] T max) where T : IComparable<T>
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (max == null)
                throw new ArgumentNullException(nameof(max));

            return value.CompareTo(max) >= 0 ? max : value;
        }

        /// <summary>
        /// Returns a random integer in range (inclusive).
        /// </summary>
        [Pure]
        public static int RandomInt(int minValue, int maxValue)
        {
            return SharedInstances.Random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// Returns a random integer smaller or equal to given value.
        /// </summary>
        [Pure]
        public static int RandomInt(int maxValue)
        {
            return SharedInstances.Random.Next(maxValue + 1);
        }

        /// <summary>
        /// Returns a random integer.
        /// </summary>
        [Pure]
        public static int RandomInt()
        {
            return SharedInstances.Random.Next();
        }

        /// <summary>
        /// Returns a random double in range.
        /// </summary>
        [Pure]
        public static double RandomDouble(double minValue, double maxValue)
        {
            return SharedInstances.Random.NextDouble() * (maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Returns a random double smaller than given.
        /// </summary>
        [Pure]
        public static double RandomDouble(double maxValue) => RandomDouble(double.MinValue, maxValue);

        /// <summary>
        /// Returns a random double.
        /// </summary>
        [Pure]
        public static double RandomDouble() => RandomDouble(double.MinValue, double.MaxValue);

        /// <summary>
        /// Returns a random bool.
        /// </summary>
        /// <param name="probability">Normalized probability of returning true (0 to 1).</param>
        [Pure]
        public static bool RandomBool(double probability)
        {
            if (probability <= 0) return false;
            if (probability >= 1) return true;
            return RandomDouble(0, 1) <= probability;
        }

        /// <summary>
        /// Returns a random bool.
        /// </summary>
        [Pure]
        public static bool RandomBool() => RandomInt(0, 1) == 1;
    }
}