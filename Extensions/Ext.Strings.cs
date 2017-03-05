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
        /// Returns true if the string is empty, null or whitespace
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => true")]
        public static bool IsBlank([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns true if the string is neither empty, null nor whitespace
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => false")]
        public static bool IsNotBlank([CanBeNull] this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Checks whether the string only consists of numeric characters
        /// </summary>
        [Pure]
        public static bool IsNumeric([NotNull] this string str)
        {
            return str.All(char.IsDigit);
        }

        /// <summary>
        /// Checks whether the string only consists of letters
        /// </summary>
        [Pure]
        public static bool IsAlphabetic([NotNull] this string str)
        {
            return str.All(char.IsLetter);
        }

        /// <summary>
        /// Checks whether the string only consists of letters and digits
        /// </summary>
        [Pure]
        public static bool IsAlphanumeric([NotNull] this string str)
        {
            return str.All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Determines whether first string equals second.
        /// Casing and culture are ignored, useless spaces are trimmed.
        /// </summary>
        [Pure]
        public static bool EqualsInvariant([CanBeNull] this string a, [CanBeNull] string b)
        {
            if (IsBlank(a) && IsBlank(b)) return true;
            if (IsBlank(a) || IsBlank(b)) return false;

            a = a.Trim();
            b = b.Trim();
            return a.Equals(b, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Determines whether first string contains second.
        /// Casing and culture are ignored, useless spaces are trimmed.
        /// </summary>
        [Pure]
        public static bool ContainsInvariant([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            if (IsBlank(str)) return IsBlank(sub);

            str = str.Trim();
            sub = sub.Trim();
            return str.IndexOf(sub, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        /// <summary>
        /// Determines whether the first string contains the second string, surrounded by spaces or linebreaks
        /// </summary>
        [Pure]
        public static bool ContainsWord([NotNull] this string str, [NotNull] string word)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (word == null)
                throw new ArgumentNullException(nameof(word));

            str = str.Trim();
            word = word.Trim();

            if (str.Equals(word, DefaultStringComparison)) return true;
            return Regex.IsMatch(str, $@"\b({Regex.Escape(word)})\b");
        }

        /// <summary>
        /// Returns null if the string is blank, otherwise returns original string
        /// </summary>
        [Pure, CanBeNull]
        [ContractAnnotation("str:null => null")]
        public static string NullIfBlank([CanBeNull] this string str)
        {
            return IsBlank(str) ? null : str;
        }

        /// <summary>
        /// Returns an empty string if given is null
        /// </summary>
        [Pure, NotNull]
        [ContractAnnotation("str:null => notnull; str:notnull => notnull")]
        public static string EmptyIfNull([CanBeNull] this string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Returns an empty string if given is blank
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
        /// Reverses the characters in a string
        /// </summary>
        [Pure, NotNull]
        public static string Reverse([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            var sb = new StringBuilder(str.Length);
            for (int i = str.Length - 1; i >= 0; i--)
                sb.Append(str[i]);
            return sb.ToString();
        }

        /// <summary>
        /// Repeats the given string <paramref name="count"/> times
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
        /// Repeats the given character <paramref name="count"/> times
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
        /// Returns the string, taking only <paramref name="charCount"/> characters
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
        /// Returns the string, skipping first <paramref name="charCount"/> characters
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
        /// Returns the string, taking only last <paramref name="charCount"/> characters
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
        /// Returns the string, skipping last <paramref name="charCount"/> characters
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
        /// Returns the given string, without any of the substrings occuring
        /// </summary>
        [Pure, NotNull]
        public static string Without([NotNull] this string str, params string[] subStrings)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            foreach (string sub in subStrings)
                str = str.Replace(sub, string.Empty);

            return str;
        }

        /// <summary>
        /// Returns the given string, without any of the characters occuring
        /// </summary>
        [Pure, NotNull]
        public static string Without([NotNull] this string str, params char[] characters)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            int pos = str.IndexOfAny(characters);
            while (pos >= 0)
            {
                str = str.Remove(pos, 1);
                pos = str.IndexOfAny(characters);
            }
            return str;
        }

        /// <summary>
        /// Makes sure the given string ends with the given other substring
        /// </summary>
        [Pure, NotNull]
        public static string EnsureEndsWith([NotNull] this string str, [NotNull] string end)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (end == null)
                throw new ArgumentNullException(nameof(end));

            return str.EndsWith(end) ? str : str + end;
        }

        /// <summary>
        /// Makes sure the given string starts with the given other substring
        /// </summary>
        [Pure, NotNull]
        public static string EnsureStartsWith([NotNull] this string str, [NotNull] string start)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (start == null)
                throw new ArgumentNullException(nameof(start));

            return str.StartsWith(start) ? str : start + str;
        }

        /// <summary>
        /// Returns substring of a string until the occurence of the other string
        /// If the other string is not found, returns full string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntil([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.IndexOf(sub, DefaultStringComparison);
            if (index < 0) return str;
            return str.Substring(0, index);
        }

        /// <summary>
        /// Returns substring of a string after the occurence of the other string
        /// If the other string is not found, returns empty string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfter([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.IndexOf(sub, DefaultStringComparison);
            if (index < 0) return string.Empty;
            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Returns substring of a string until the last occurence of the other string
        /// If the other string is not found, returns full string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntilLast([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.LastIndexOf(sub, DefaultStringComparison);
            if (index < 0) return str;
            return str.Substring(0, index);
        }

        /// <summary>
        /// Returns substring of a string after the last occurence of the other string
        /// If the other string is not found, returns empty string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfterLast([NotNull] this string str, [NotNull] string sub)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            int index = str.LastIndexOf(sub, DefaultStringComparison);
            if (index < 0) return string.Empty;
            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Determines whether the string enumerable contains second string.
        /// Casing and culture are ignored, useless spaces are trimmed.
        /// </summary>
        [Pure]
        public static bool ContainsInvariant([NotNull] this IEnumerable<string> enumerable, [CanBeNull] string sub)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Any(sub.EqualsInvariant);
        }

        /// <summary>
        /// Filters out blank strings from an enumerable
        /// </summary>
        public static IEnumerable<string> WithoutBlank([NotNull] this IEnumerable<string> enumerable)
        {
            return enumerable.Where(IsNotBlank);
        }

        /// <summary>
        /// Strips the longest common string for the given enumerable of string, which has its origin at string start
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<string> StripCommonStart([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var array = enumerable as string[] ?? enumerable.ToArray();
            if (!array.Any()) return array;

            // Get the longest common string
            string common = array[0];
            while (common.IsNotBlank() && !array.All(s => s.StartsWith(common)))
                common = common.SkipLast(1);

            // Strip input strings
            return common.IsNotBlank()
                ? array.Select(s => s.Skip(common.Length))
                : array;
        }

        /// <summary>
        /// Strips the longest common string for the given enumerable of string, which has its origin at string end
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<string> StripCommonEnd([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var array = enumerable as string[] ?? enumerable.ToArray();
            if (!array.Any()) return array;

            // Get the longest common string
            string common = array[0];
            while (common.IsNotBlank() && !array.All(s => s.EndsWith(common)))
                common = common.Skip(1);

            // Strip input strings
            return common.IsNotBlank()
                ? array.Select(s => s.SkipLast(common.Length))
                : array;
        }

        /// <summary>
        /// Trims strings in an enumerable
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static IEnumerable<string> TrimAll([NotNull] this IEnumerable<string> enumerable, [NotNull] params char[] trimChars)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (trimChars == null)
                throw new ArgumentNullException(nameof(trimChars));

            return enumerable.Select(str => str.Trim(trimChars));
        }

        /// <summary>
        /// Trims strings in an enumerable
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static IEnumerable<string> TrimAll([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Select(str => str.Trim());
        }

        /// <summary>
        /// Split string into substrings using separator strings, removing empty strings
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
        /// Split string into substrings using separator characters, removing empty strings
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
        /// Split string into substrings using separator string.
        /// Empty strings are removed and existing are trimmed.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] SplitTrim([NotNull] this string str, [NotNull] string separator, [NotNull] params char[] trimChars)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));
            if (trimChars == null)
                throw new ArgumentNullException(nameof(trimChars));

            return str
                .Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(sub => sub.Trim(trimChars))
                .Where(IsNotBlank)
                .ToArray();
        }

        /// <summary>
        /// Split string into substrings using separator string.
        /// Empty strings are removed and existing are trimmed.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] SplitTrim([NotNull] this string str, [NotNull] string separator)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            return str
                .Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(sub => sub.Trim())
                .Where(IsNotBlank)
                .ToArray();
        }

        /// <summary>
        /// Joins members of an <see cref="IEnumerable{T}"/> into a string, separated by given string
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> enumerable, string separator = ", ")
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return string.Join(separator, enumerable);
        }

        #region Parse methods
        /// <summary>
        /// Parses the string into an object of generic type using a <see cref="ParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T Parse<T>([NotNull] this string str, [NotNull] ParseDelegate<T> handler)
        {
            if (IsBlank(str))
                throw new ArgumentNullException(nameof(str));
            if (handler == null)
                throw new ArgumentNullException(nameof(handler));

            return handler(str);
        }

        /// <summary>
        /// Parses the string into an object of generic type using a <see cref="TryParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T ParseOrDefault<T>([CanBeNull] this string str, [NotNull] TryParseDelegate<T> handler, T defaultValue = default(T))
        {
            if (IsBlank(str))
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
        /// Parses the string into DateTime
        /// </summary>
        [Pure]
        public static DateTime ParseDateTime([NotNull] this string str)
            => Parse(str, DateTime.Parse);

        /// <summary>
        /// Parses the string into DateTime or returns the default value if it fails
        /// </summary>
        [Pure]
        public static DateTime ParseDateTimeOrDefault(this string str, DateTime defaultValue = default(DateTime))
            => ParseOrDefault(str, DateTime.TryParse, defaultValue);
        #endregion

        /// <summary>
        /// Trims trailing zeroes in the version object and returns its string representation
        /// </summary>
        [Pure, NotNull]
        public static string TrimToString([NotNull] this Version version)
        {
            if (version == null)
                throw new ArgumentNullException(nameof(version));

            if (version.Revision <= 0)
            {
                if (version.Build <= 0)
                {
                    if (version.Minor <= 0)
                    {
                        return $"{version.Major}";
                    }
                    return $"{version.Major}.{version.Minor}";
                }
                return $"{version.Major}.{version.Minor}.{version.Build}";
            }
            return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
        }
    }
}