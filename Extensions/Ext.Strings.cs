using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Determines whether the string is either null, empty or whitespace
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => true")]
        public static bool IsBlank([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Determines whether the string is neither null, empty or whitespace
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => false")]
        public static bool IsNotBlank([CanBeNull] this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Determines whether the string only consists of digits
        /// </summary>
        [Pure]
        public static bool IsNumeric([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether the string only consists of letters
        /// </summary>
        [Pure]
        public static bool IsAlphabetic([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsLetter);
        }

        /// <summary>
        /// Determines whether the string only consists of letters and/or digits
        /// </summary>
        [Pure]
        public static bool IsAlphanumeric([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Determines whether the strings are equal.
        /// Ignores casing and leading/trailing whitespace.
        /// </summary>
        [Pure]
        public static bool EqualsInvariant([CanBeNull] this string a, [CanBeNull] string b)
        {
            a = a?.Trim();
            b = b?.Trim();
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether one string contains another.
        /// Ignores casing and leading/trailing whitespace.
        /// </summary>
        [Pure]
        public static bool ContainsInvariant([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            str = str.Trim();
            sub = sub.Trim();
            return str.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>
        /// Determines whether one string contains another string, surrounded by word boundaries
        /// </summary>
        [Pure]
        public static bool ContainsWord([NotNull] this string str, [NotNull] string word,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (word == null)
                throw new ArgumentNullException(nameof(word));

            if (string.Equals(str, word, comparison)) return true;
            return Regex.IsMatch(str, $@"\b({Regex.Escape(word)})\b");
        }

        /// <summary>
        /// Returns null if the given string is either null, empty or whitespace, otherwise returns the same string
        /// </summary>
        [Pure, CanBeNull]
        [ContractAnnotation("str:null => null")]
        public static string NullIfBlank([CanBeNull] this string str)
        {
            return IsBlank(str) ? null : str;
        }

        /// <summary>
        /// Returns an empty string if the given string is null, otherwise returns the same string
        /// </summary>
        [Pure, NotNull]
        [ContractAnnotation("str:null => notnull; str:notnull => notnull")]
        public static string EmptyIfNull([CanBeNull] this string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Returns an empty string if the given string is either null, empty or whitespace, otherwise returns the same string
        /// </summary>
        [Pure, NotNull]
        [ContractAnnotation("str:null => notnull; str:notnull => notnull")]
        public static string EmptyIfBlank([CanBeNull] this string str)
        {
            return IsBlank(str) ? string.Empty : str;
        }

        /// <summary>
        /// Formats the given string identically to <see cref="string.Format(string,object[])"/>
        /// </summary>
        [Pure, NotNull, StringFormatMethod("str")]
        public static string Format([NotNull] this string str, params object[] args)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return string.Format(str, args);
        }

        /// <summary>
        /// Removes all leading occurrences of a substring in the given string
        /// </summary>
        [Pure, NotNull]
        public static string TrimStart([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            while (str.StartsWith(sub, comparison))
                str = str.Substring(sub.Length);

            return str;
        }

        /// <summary>
        /// Removes all trailing occurrences of a substring in the given string
        /// </summary>
        [Pure, NotNull]
        public static string TrimEnd([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            while (str.EndsWith(sub, comparison))
                str = str.Substring(0, str.Length - sub.Length);

            return str;
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a substring in the given string
        /// </summary>
        [Pure, NotNull]
        public static string Trim([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            return str.TrimStart(sub).TrimEnd(sub);
        }

        /// <summary>
        /// Reverses order of characters in a string
        /// </summary>
        [Pure, NotNull]
        public static string Reverse([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (str.Length < 2)
                return str;

            var sb = new StringBuilder(str.Length);
            for (int i = str.Length - 1; i >= 0; i--)
                sb.Append(str[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given string given number of times
        /// </summary>
        [Pure, NotNull]
        public static string Repeat([NotNull] this string str, int count)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (count == 0)
                return string.Empty;

            // Optimization
            if (count == 1)
                return str;
            if (count == 2)
                return str + str;
            if (count == 3)
                return str + str + str;

            // StringBuilder for count >= 4
            var sb = new StringBuilder(str, str.Length*count);
            for (int i = 2; i <= count; i++)
                sb.Append(str);

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given character given number of times
        /// </summary>
        [Pure, NotNull]
        public static string Repeat(this char c, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (count == 0)
                return string.Empty;

            return new string(c, count);
        }

        /// <summary>
        /// Truncates a string leaving only the given number of characters from the start of the string
        /// </summary>
        [Pure, NotNull]
        public static string Take([NotNull] this string str, int charCount)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (charCount < 0)
                throw new ArgumentOutOfRangeException(nameof(charCount), "Cannot be negative");

            if (charCount == 0) return string.Empty;
            if (charCount >= str.Length) return str;
            return str.Substring(0, charCount);
        }

        /// <summary>
        /// Truncates a string dropping the given number of characters from the start of the string
        /// </summary>
        [Pure, NotNull]
        public static string Skip([NotNull] this string str, int charCount)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (charCount < 0)
                throw new ArgumentOutOfRangeException(nameof(charCount), "Cannot be negative");

            if (charCount == 0) return str;
            if (charCount >= str.Length) return string.Empty;
            return str.Substring(charCount);
        }

        /// <summary>
        /// Truncates a string leaving only the given number of characters from the end of the string
        /// </summary>
        [Pure, NotNull]
        public static string TakeLast([NotNull] this string str, int charCount)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (charCount < 0)
                throw new ArgumentOutOfRangeException(nameof(charCount), "Cannot be negative");

            if (charCount == 0) return string.Empty;
            if (charCount >= str.Length) return str;
            return Skip(str, str.Length - charCount);
        }

        /// <summary>
        /// Truncates a string dropping the given number of characters from the end of the string
        /// </summary>
        [Pure, NotNull]
        public static string SkipLast([NotNull] this string str, int charCount)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (charCount < 0)
                throw new ArgumentOutOfRangeException(nameof(charCount), "Cannot be negative");

            if (charCount == 0) return str;
            if (charCount >= str.Length) return string.Empty;
            return Take(str, str.Length - charCount);
        }

        /// <summary>
        /// Removes all occurrences of the given substrings from a string
        /// </summary>
        [Pure, NotNull]
        public static string Except([NotNull] this string str, [NotNull] IEnumerable<string> substrings,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (substrings == null)
                throw new ArgumentNullException(nameof(substrings));

            foreach (string sub in substrings)
            {
                int index = str.IndexOf(sub, comparison);
                while (index >= 0)
                {
                    str = str.Remove(index, sub.Length);
                    index = str.IndexOf(sub, comparison);
                }
            }

            return str;
        }

        /// <summary>
        /// Removes all occurrences of the given substrings from a string
        /// </summary>
        [Pure, NotNull]
        public static string Except([NotNull] this string str, params string[] substrings)
            => Except(str, (IEnumerable<string>) substrings);

        /// <summary>
        /// Removes all occurrences of the given characters from a string
        /// </summary>
        [Pure, NotNull]
        public static string Except([NotNull] this string str, [NotNull] IEnumerable<char> characters)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (characters == null)
                throw new ArgumentNullException(nameof(characters));

            var charArray = characters as char[] ?? characters.ToArray();
            int pos = str.IndexOfAny(charArray);
            while (pos >= 0)
            {
                str = str.Remove(pos, 1);
                pos = str.IndexOfAny(charArray);
            }
            return str;
        }

        /// <summary>
        /// Removes all occurrences of the given characters from a string
        /// </summary>
        [Pure, NotNull]
        public static string Except([NotNull] this string str, params char[] characters)
            => Except(str, (IEnumerable<char>) characters);

        /// <summary>
        /// Prepends a string with the given string if it doesn't start with it already
        /// </summary>
        [Pure, NotNull]
        public static string EnsureStartsWith([NotNull] this string str, [NotNull] string start,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (start == null)
                throw new ArgumentNullException(nameof(start));

            return str.StartsWith(start, comparison) ? str : start + str;
        }

        /// <summary>
        /// Appends a string with the given string if it doesn't end with it already
        /// </summary>
        [Pure, NotNull]
        public static string EnsureEndsWith([NotNull] this string str, [NotNull] string end,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (end == null)
                throw new ArgumentNullException(nameof(end));

            return str.EndsWith(end, comparison) ? str : str + end;
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of first occurrence of the given other string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntil([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.IndexOf(sub, comparison);
            if (index < 0) return str;
            return str.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of first occurrence of the given other string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfter([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.IndexOf(sub, comparison);
            if (index < 0) return string.Empty;
            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of last occurrence of the given other string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntilLast([NotNull] this string str, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.LastIndexOf(sub, comparsion);
            if (index < 0) return str;
            return str.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of last occurrence of the given other string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfterLast([NotNull] this string str, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.LastIndexOf(sub, comparsion);
            if (index < 0) return string.Empty;
            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Determines whether the string enumerable contains a string.
        /// Ignores casing and leading/trailing whitespace.
        /// </summary>
        [Pure]
        public static bool ContainsInvariant([NotNull] this IEnumerable<string> enumerable, [CanBeNull] string sub)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Any(sub.EqualsInvariant);
        }

        /// <summary>
        /// Discards blank strings from a sequence
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<string> ExceptBlank([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Where(IsNotBlank);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] Split([NotNull] this string str, [NotNull] params string[] separators)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (separators == null)
                throw new ArgumentNullException(nameof(separators));

            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] Split([NotNull] this string str, [NotNull] params char[] separators)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (separators == null)
                throw new ArgumentNullException(nameof(separators));

            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a string formed by joining elements of a sequence using the given separator
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] string separator)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            return string.Join(separator, enumerable);
        }

        /// <summary>
        /// Returns a string formed by joining elements of a sequence using the given separator
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> enumerable, char separator)
            => JoinToString(enumerable, separator.ToString());

        #region Parse methods

        /// <summary>
        /// Parses the string into an object of given type using a <see cref="ParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T Parse<T>([NotNull] this string str, [NotNull] ParseDelegate<T> handler)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return handler(str);
        }

        /// <summary>
        /// Parses the string into an object of given type using a <see cref="TryParseDelegate{T}"/> handler or returns default value if unsuccessful
        /// </summary>
        [Pure]
        public static T ParseOrDefault<T>([CanBeNull] this string str, [NotNull] TryParseDelegate<T> handler,
            T defaultValue = default(T))
        {
            if (str == null)
                return defaultValue;
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

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
        /// Parses the string into float or returns default value if unsuccessful
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
        /// Parses the string into double or returns default value if unsuccessful
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
        /// Parses the string into decimal or returns default value if unsuccessful
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
        /// Parses the string into short or returns default value if unsuccessful
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
        /// Parses the string into int or returns default value if unsuccessful
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
        /// Parses the string into long or returns default value if unsuccessful
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
        /// Parses the string into byte or returns default value if unsuccessful
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
        /// Parses the string into uint or returns default value if unsuccessful
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
        /// Parses the string into ulong or returns default value if unsuccessful
        /// </summary>
        [Pure]
        public static ulong ParseUlongOrDefault(this string str, ulong defaultValue = default(ulong))
            => ParseOrDefault(str, ulong.TryParse, defaultValue);

        /// <summary>
        /// Parses the string into DateTime
        /// </summary>
        [Pure]
        public static DateTime ParseDateTime([NotNull] this string str)
            => Parse(str, DateTime.Parse);

        /// <summary>
        /// Parses the string into DateTime or returns default value if unsuccessful
        /// </summary>
        [Pure]
        public static DateTime ParseDateTimeOrDefault(this string str, DateTime defaultValue = default(DateTime))
            => ParseOrDefault(str, DateTime.TryParse, defaultValue);

        #endregion
    }
}