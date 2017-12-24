using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Determines whether the string is either null, empty or whitespace.
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => true")]
        public static bool IsBlank([CanBeNull] this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Determines whether the string is neither null, empty or whitespace.
        /// </summary>
        [Pure]
        [ContractAnnotation("str:null => false")]
        public static bool IsNotBlank([CanBeNull] this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Determines whether the string only consists of digits.
        /// </summary>
        [Pure]
        public static bool IsNumeric([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether the string only consists of letters.
        /// </summary>
        [Pure]
        public static bool IsAlphabetic([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsLetter);
        }

        /// <summary>
        /// Determines whether the string only consists of letters and/or digits.
        /// </summary>
        [Pure]
        public static bool IsAlphanumeric([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return str.ToCharArray().All(char.IsLetterOrDigit);
        }

        /// <summary>
        /// Returns null if the given string is either null, empty or whitespace, otherwise returns the same string.
        /// </summary>
        [Pure, CanBeNull]
        [ContractAnnotation("str:null => null")]
        public static string NullIfBlank([CanBeNull] this string str)
        {
            return IsBlank(str) ? null : str;
        }

        /// <summary>
        /// Returns an empty string if the given string is null, otherwise returns the same string.
        /// </summary>
        [Pure, NotNull]
        public static string EmptyIfNull([CanBeNull] this string str)
        {
            return str ?? string.Empty;
        }

        /// <summary>
        /// Returns an empty string if the given string is either null, empty or whitespace, otherwise returns the same string.
        /// </summary>
        [Pure, NotNull]
        public static string EmptyIfBlank([CanBeNull] this string str)
        {
            return IsBlank(str) ? string.Empty : str;
        }

        /// <summary>
        /// Formats the given string identically to <see cref="string.Format(string,object[])"/>.
        /// </summary>
        [Pure, NotNull, StringFormatMethod("str")]
        public static string Format([NotNull] this string str, params object[] args)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return string.Format(str, args);
        }

        /// <summary>
        /// Removes all leading occurrences of a substring in the given string.
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
        /// Removes all trailing occurrences of a substring in the given string.
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
        /// Removes all leading and trailing occurrences of a substring in the given string.
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
        /// Reverses order of characters in a string.
        /// </summary>
        [Pure, NotNull]
        public static string Reverse([NotNull] this string str)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            if (str.Length <= 1)
                return str;

            var sb = new StringBuilder(str.Length);
            for (var i = str.Length - 1; i >= 0; i--)
                sb.Append(str[i]);

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given string given number of times.
        /// </summary>
        [Pure, NotNull]
        public static string Repeat([NotNull] this string str, int count)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

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
            var sb = new StringBuilder(str, str.Length * count);
            for (var i = 2; i <= count; i++)
                sb.Append(str);

            return sb.ToString();
        }

        /// <summary>
        /// Returns a string formed by repeating the given character given number of times.
        /// </summary>
        [Pure, NotNull]
        public static string Repeat(this char c, int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count));

            if (count == 0)
                return string.Empty;

            return new string(c, count);
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
        /// </summary>
        [Pure, NotNull]
        public static string Replace([NotNull] this string str, [NotNull] string oldValue, [NotNull] string newValue,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (oldValue == null)
                throw new ArgumentNullException(nameof(oldValue));
            if (newValue == null)
                throw new ArgumentNullException(nameof(newValue));

            var index = str.IndexOf(oldValue, comparison);
            while (index >= 0)
            {
                str = str.Remove(index, oldValue.Length);
                str = str.Insert(index, newValue);
                index = str.IndexOf(oldValue, comparison);
            }

            return str;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified strings in the current instance are replaced with another specified string.
        /// </summary>
        [Pure, NotNull]
        public static string Replace([NotNull] this string str, [NotNull] IEnumerable<string> oldValues,
            [NotNull] string newValue, StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (oldValues == null)
                throw new ArgumentNullException(nameof(oldValues));

            foreach (var oldValue in oldValues)
                str = str.Replace(oldValue, newValue, comparison);

            return str;
        }

        /// <summary>
        /// Returns a new string in which all occurrences of specified characters in the current instance are replaced with another specified character.
        /// </summary>
        [Pure, NotNull]
        public static string Replace([NotNull] this string str, [NotNull] IEnumerable<char> oldChars, char newChar)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (oldChars == null)
                throw new ArgumentNullException(nameof(oldChars));

            var charArray = oldChars as char[] ?? oldChars.ToArray();
            var pos = str.IndexOfAny(charArray);
            while (pos >= 0)
            {
                str = str.Remove(pos, 1);
                str = str.Insert(pos, newChar.ToString());
                pos = str.IndexOfAny(charArray);
            }

            return str;
        }

        /// <summary>
        /// Prepends a string if the given string doesn't start with it already.
        /// </summary>
        [Pure, NotNull]
        public static string EnsureStartsWith([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            return str.StartsWith(sub, comparison) ? str : sub + str;
        }

        /// <summary>
        /// Appends a string if the given string doesn't end with it already.
        /// </summary>
        [Pure, NotNull]
        public static string EnsureEndsWith([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            return str.EndsWith(sub, comparison) ? str : str + sub;
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of first occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntil([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            var index = str.IndexOf(sub, comparison);
            if (index < 0) return str;

            return str.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of first occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfter([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            var index = str.IndexOf(sub, comparison);
            if (index < 0) return string.Empty;

            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of last occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntilLast([NotNull] this string str, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            var index = str.LastIndexOf(sub, comparsion);
            if (index < 0) return str;

            return str.Substring(0, index);
        }

        /// <summary>
        /// Retrieves a substring that starts at the position of last occurrence of the given other string.
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfterLast([NotNull] this string str, [NotNull] string sub,
            StringComparison comparsion = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));

            var index = str.LastIndexOf(sub, comparsion);
            if (index < 0) return string.Empty;

            return str.Substring(index + sub.Length, str.Length - index - sub.Length);
        }

        /// <summary>
        /// Discards blank strings from a sequence.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static IEnumerable<string> ExceptBlank([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Where(IsNotBlank);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries.
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
        /// Splits string using given separators, discarding empty entries.
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
        /// Returns a string formed by joining elements of a sequence using the given separator.
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
    }
}