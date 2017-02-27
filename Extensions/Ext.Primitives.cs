using System;
using System.Collections;
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
        public static bool IsEither<T>(this object obj, params T[] objs)
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
        /// Parses the string into an object of generic type using a <see cref="ParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T Parse<T>([NotNull] this string str, ParseDelegate<T> handler)
        {
            if (IsBlank(str)) throw new ArgumentNullException(nameof(str));
            return handler(str);
        }

        /// <summary>
        /// Parses the string into an object of generic type using a <see cref="TryParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T ParseOrDefault<T>(this string str, TryParseDelegate<T> handler, T defaultValue = default(T))
        {
            if (IsBlank(str)) return defaultValue;
            T result;
            return handler(str, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Parses the string into float
        /// </summary>
        [Pure]
        public static float ParseFloat([NotNull] this string str)
            => Parse(str, float.Parse);

        /// <summary>
        /// Parses the string into float or returns the default value if it fails
        /// </summary>
        [Pure]
        public static float ParseFloatOrDefault(this string str, float defaultValue = default(float))
            => ParseOrDefault(str, float.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into double
        /// </summary>
        [Pure]
        public static double ParseDouble([NotNull] this string str)
            => Parse(str, double.Parse);

        /// <summary>
        /// Parses the string into double or returns the default value if it fails
        /// </summary>
        [Pure]
        public static double ParseDoubleOrDefault(this string str, double defaultValue = default(double))
            => ParseOrDefault(str, double.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into decimal
        /// </summary>
        [Pure]
        public static decimal ParseDecimal([NotNull] this string str)
            => Parse(str, decimal.Parse);

        /// <summary>
        /// Parses the string into decimal or returns the default value if it fails
        /// </summary>
        [Pure]
        public static decimal ParseDecimalOrDefault(this string str, decimal defaultValue = default(decimal))
            => ParseOrDefault(str, decimal.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into short
        /// </summary>
        [Pure]
        public static short ParseShort([NotNull] this string str)
            => Parse(str, short.Parse);

        /// <summary>
        /// Parses the string into short or returns the default value if it fails
        /// </summary>
        [Pure]
        public static short ParseShortOrDefault(this string str, short defaultValue = default(short))
            => ParseOrDefault(str, short.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into int
        /// </summary>
        [Pure]
        public static int ParseInt([NotNull] this string str)
            => Parse(str, int.Parse);

        /// <summary>
        /// Parses the string into int or returns the default value if it fails
        /// </summary>
        [Pure]
        public static int ParseIntOrDefault(this string str, int defaultValue = default(int))
            => ParseOrDefault(str, int.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into long
        /// </summary>
        [Pure]
        public static long ParseLong([NotNull] this string str)
            => Parse(str, long.Parse);

        /// <summary>
        /// Parses the string into long or returns the default value if it fails
        /// </summary>
        [Pure]
        public static long ParseLongOrDefault(this string str, long defaultValue = default(long))
            => ParseOrDefault(str, long.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into byte
        /// </summary>
        [Pure]
        public static byte ParseByte([NotNull] this string str)
            => Parse(str, byte.Parse);

        /// <summary>
        /// Parses the string into byte or returns the default value if it fails
        /// </summary>
        [Pure]
        public static byte ParseByteOrDefault(this string str, byte defaultValue = default(byte))
            => ParseOrDefault(str, byte.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into uint
        /// </summary>
        [Pure]
        public static uint ParseUint([NotNull] this string str)
            => Parse(str, uint.Parse);

        /// <summary>
        /// Parses the string into uint or returns the default value if it fails
        /// </summary>
        [Pure]
        public static uint ParseUintOrDefault(this string str, uint defaultValue = default(uint))
            => ParseOrDefault(str, uint.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into ulong
        /// </summary>
        [Pure]
        public static ulong ParseUlong([NotNull] this string str)
            => Parse(str, ulong.Parse);

        /// <summary>
        /// Parses the string into ulong or returns the default value if it fails
        /// </summary>
        [Pure]
        public static ulong ParseUlongOrDefault(this string str, ulong defaultValue = default(ulong))
            => ParseOrDefault(str, ulong.TryParse, defaultValue);

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