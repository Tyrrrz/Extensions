using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Converts an object to one of another type
        /// </summary>
        [Pure]
        public static T ConvertTo<T>(this object obj)
        {
            return (T) Convert.ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// Converts an object to one of another type
        /// </summary>
        [Pure]
        public static object ConvertTo(this object obj, Type newType)
        {
            return Convert.ChangeType(obj, newType);
        }

        /// <summary>
        /// Tries to convert an object to a different type, returns the new value if successful or default value if not
        /// </summary>
        [Pure]
        public static T ConvertOrDefault<T>(this object obj, T defaultValue = default(T))
        {
            try
            {
                return (T) Convert.ChangeType(obj, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Returns true if the object is equal to one of the items in an <see cref="IEnumerable{T}"/>
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            foreach (var other in enumerable)
            {
                if (Equals(obj, other))
                    return true;
            }
            return false;
        }
        
        /// <summary>
        /// Returns true if the object is equal to one of the parameters
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, params T[] objs)
        {
            foreach (var other in objs)
            {
                if (Equals(obj, other))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the value is in given range
        /// </summary>
        [Pure]
        public static bool IsInRange(this IComparable value, IComparable min, IComparable max)
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        /// <summary>
        /// Makes sure the value is in given range
        /// </summary>
        [Pure]
        public static T Clamp<T>(this T value, T min, T max) where T : IComparable
        {
            return value.CompareTo(min) <= 0 ? min : value.CompareTo(max) >= 0 ? max : value;
        }

        /// <summary>
        /// Makes sure the value is not lower than given minimum
        /// </summary>
        [Pure]
        public static T ClampMin<T>(this T value, T min) where T : IComparable
        {
            return value.CompareTo(min) <= 0 ? min : value;
        }

        /// <summary>
        /// Makes sure the value is not higher than given maximum
        /// </summary>
        [Pure]
        public static T ClampMax<T>(this T value, T max) where T : IComparable
        {
            return value.CompareTo(max) >= 0 ? max : value;
        }

        /// <summary>
        /// Ceils the double and returns an int
        /// </summary>
        [Pure]
        public static int CeilingToInt(this double value)
        {
            return (int) Math.Ceiling(value);
        }

        /// <summary>
        /// Floors the double and return an int
        /// </summary>
        [Pure]
        public static int FloorToInt(this double value)
        {
            return (int) Math.Floor(value);
        }

        /// <summary>
        /// Returns the fractional point of a double
        /// </summary>
        [Pure]
        public static double Fraction(this double value)
        {
            return value - FloorToInt(value);
        }

        /// <summary>
        /// Returns a random integer
        /// </summary>
        [Pure]
        public static int RandomInt()
        {
            return SharedInstances.Random.Next();
        }

        /// <summary>
        /// Returns a random integer, smaller or equal to given
        /// </summary>
        [Pure]
        public static int RandomInt(int maxValue)
        {
            return SharedInstances.Random.Next(maxValue + 1);
        }

        /// <summary>
        /// Returns a random integer in range (inclusive)
        /// </summary>
        [Pure]
        public static int RandomInt(int minValue, int maxValue)
        {
            return SharedInstances.Random.Next(minValue, maxValue + 1);
        }

        /// <summary>
        /// Returns a random double
        /// </summary>
        [Pure]
        public static double RandomDouble()
        {
            return RandomDouble(double.MinValue, double.MaxValue);
        }

        /// <summary>
        /// Returns a random double, smaller than given
        /// </summary>
        [Pure]
        public static double RandomDouble(double maxValue)
        {
            return RandomDouble(double.MinValue, maxValue);
        }

        /// <summary>
        /// Returns a random double in range
        /// </summary>
        /// <returns></returns>
        [Pure]
        public static double RandomDouble(double minValue, double maxValue)
        {
            return SharedInstances.Random.NextDouble()*(maxValue - minValue) + minValue;
        }

        /// <summary>
        /// Returns a random bool
        /// </summary>
        /// <param name="probability">Probability of returning "true" (0 to 1)</param>
        [Pure]
        public static bool RandomBool(double probability)
        {
            if (probability <= 0) return false;
            if (probability >= 1) return true;
            return RandomDouble(0, 1) <= probability;
        }

        /// <summary>
        /// Returns a random bool
        /// </summary>
        [Pure]
        public static bool RandomBool()
        {
            return RandomInt(0, 1) == 1;
        }
    }
}