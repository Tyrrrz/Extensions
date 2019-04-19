using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="string" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether a string is null or empty.
        /// </summary>
        [Pure]
        [ContractAnnotation("s:null => true")]
        public static bool IsNullOrEmpty([CanBeNull] this string s) => string.IsNullOrEmpty(s);

        /// <summary>
        /// Indicates whether a string is either null, empty, or whitespace.
        /// </summary>
        [Pure]
        [ContractAnnotation("s:null => true")]
        public static bool IsNullOrWhiteSpace([CanBeNull] this string s) => string.IsNullOrWhiteSpace(s);

        /// <summary>
        /// Returns an empty string if given a null string, otherwise returns given string.
        /// </summary>
        [Pure, NotNull]
        [ContractAnnotation("s:null => notnull")]
        public static string EmptyIfNull([CanBeNull] this string s) => s ?? string.Empty;

        /// <summary>
        /// Determines whether the string only consists of digits.
        /// </summary>
        [Pure]
        public static bool IsNumeric([NotNull] this string s)
        {
            s.GuardNotNull(nameof(s));
            return s.ToCharArray().All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether the string only consists of letters.
        /// </summary>
        [Pure]
        public static bool IsAlphabetic([NotNull] this string s)
        {
            s.GuardNotNull(nameof(s));
            return s.ToCharArray().All(char.IsLetter);
        }

        /// <summary>
        /// Determines whether the string only consists of letters and/or digits.
        /// </summary>
        [Pure]
        public static bool IsAlphanumeric([NotNull] this string s)
        {
            s.GuardNotNull(nameof(s));
            return s.ToCharArray().All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Removes all leading occurrences of a substring in the given string.
        /// </summary>
        [Pure, NotNull]
        public static string TrimStart([NotNull] this string s, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            while (s.StartsWith(sub, comparison))
                s = s.Substring(sub.Length);

            return s;
        }

        /// <summary>
        /// Removes all trailing occurrences of a substring in the given string.
        /// </summary>
        [Pure, NotNull]
        public static string TrimEnd([NotNull] this string s, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            while (s.EndsWith(sub, comparison))
                s = s.Substring(0, s.Length - sub.Length);

            return s;
        }

        /// <summary>
        /// Removes all leading and trailing occurrences of a substring in the given string.
        /// </summary>
        [Pure, NotNull]
        public static string Trim([NotNull] this string s, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            return s.TrimStart(sub, comparison).TrimEnd(sub, comparison);
        }

        /// <summary>
        /// Reverses order of characters in a string.
        /// </summary>
        [Pure, NotNull]
        public static string Reverse([NotNull] this string s)
        {
            s.GuardNotNull(nameof(s));

            // If length is 1 char or less - return same string
            if (s.Length <= 1)
                return s;

            // Concat a new string
            var sb = new StringBuilder(s.Length);
            for (var i = s.Length - 1; i >= 0; i--)
                sb.Append(s[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given string given number of times.
        /// </summary>
        [Pure, NotNull]
        public static string Repeat([NotNull] this string s, int count)
        {
            s.GuardNotNull(nameof(s));
            count.GuardNotNegative(nameof(count));

            // If count is 0 - return empty string
            if (count == 0)
                return string.Empty;

            // Concat a new string
            var sb = new StringBuilder(s.Length * count);
            for (var i = 0; i < count; i++)
                sb.Append(s);

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given character given number of times.
        /// </summary>
        [Pure, NotNull]
        public static string Repeat(this char c, int count)
        {
            count.GuardNotNegative(nameof(count));

            // If count is 0 - return empty string
            if (count == 0)
                return string.Empty;

            return new string(c, count);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
        /// </summary>
        [Pure, NotNull]
        public static string Replace([NotNull] this string s, [NotNull] string oldValue, [NotNull] string newValue,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            oldValue.GuardNotNull(nameof(oldValue));
            newValue.GuardNotNull(nameof(newValue));

            var sb = new StringBuilder();

            var offset = 0;
            while (true)
            {
                // Find the next occurence of old value
                var index = s.IndexOf(oldValue, offset, comparison);

                // If not found - append the rest of the string and return
                if (index < 0)
                {
                    sb.Append(s, offset, s.Length - offset);
                    return sb.ToString();
                }

                // Append a portion of the string since last occurence until this one
                sb.Append(s, offset, index - offset);

                // Append new value
                sb.Append(newValue);

                // Advance offset
                offset = index + oldValue.Length;
            }
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of first occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntil([NotNull] this string s, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            // Find substring
            var index = s.IndexOf(sub, comparison);

            // If not found - return whole string
            if (index < 0)
                return s;

            // Otherwise - return portion of the string until index
            return s.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of first occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfter([NotNull] this string s, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            // Find substring
            var index = s.IndexOf(sub, comparison);

            // If not found - return empty string
            if (index < 0)
                return string.Empty;

            // Otherwise - return portion of the string after index
            return s.Substring(index + sub.Length, s.Length - index - sub.Length);
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of last occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntilLast([NotNull] this string s, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            // Find substring
            var index = s.LastIndexOf(sub, comparsion);

            // If not found - return whole string
            if (index < 0)
                return s;

            // Otherwise - return portion of the string until index
            return s.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of last occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfterLast([NotNull] this string s, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            s.GuardNotNull(nameof(s));
            sub.GuardNotNull(nameof(sub));

            // Find substring
            var index = s.LastIndexOf(sub, comparsion);

            // If not found - return empty string
            if (index < 0)
                return string.Empty;

            // Otherwise - return portion of the string after index
            return s.Substring(index + sub.Length, s.Length - index - sub.Length);
        }

        /// <summary>
        /// Discards null, empty and whitespace strings from a sequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<string> ExceptNullOrWhiteSpace([NotNull] this IEnumerable<string> source)
        {
            source.GuardNotNull(nameof(source));
            return source.Where(s => !IsNullOrWhiteSpace(s));
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries.
        /// </summary>
        [Pure, NotNull]
        public static string[] Split([NotNull] this string s, [NotNull] params string[] separators)
        {
            s.GuardNotNull(nameof(s));
            return s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries.
        /// </summary>
        [Pure, NotNull]
        public static string[] Split([NotNull] this string s, [NotNull] params char[] separators)
        {
            s.GuardNotNull(nameof(s));
            return s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a string formed by joining elements of a sequence using the given separator.
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> source, [NotNull] string separator)
        {
            source.GuardNotNull(nameof(source));
            separator.GuardNotNull(nameof(separator));

            return string.Join(separator, source);
        }
    }
}