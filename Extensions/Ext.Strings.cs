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
            GuardNull(str, nameof(str));

            return str.ToCharArray().All(char.IsDigit);
        }

        /// <summary>
        /// Determines whether the string only consists of letters
        /// </summary>
        [Pure]
        public static bool IsAlphabetic([NotNull] this string str)
        {
            GuardNull(str, nameof(str));

            return str.ToCharArray().All(char.IsLetter);
        }

        /// <summary>
        /// Determines whether the string only consists of letters and/or digits
        /// </summary>
        [Pure]
        public static bool IsAlphanumeric([NotNull] this string str)
        {
            GuardNull(str, nameof(str));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(word, nameof(word));

            if (string.Equals(str, word, comparison)) return true;

            var regexOptions = RegexOptions.None;
            if (comparison == StringComparison.CurrentCultureIgnoreCase ||
                comparison == StringComparison.OrdinalIgnoreCase)
                regexOptions |= RegexOptions.IgnoreCase;
            if (comparison == StringComparison.Ordinal)
                regexOptions |= RegexOptions.CultureInvariant;

            return Regex.IsMatch(str, $@"\b({Regex.Escape(word)})\b", regexOptions);
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
            GuardNull(str, nameof(str));

            return string.Format(str, args);
        }

        /// <summary>
        /// Removes all leading occurrences of a substring in the given string
        /// </summary>
        [Pure, NotNull]
        public static string TrimStart([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

            return str.TrimStart(sub).TrimEnd(sub);
        }

        /// <summary>
        /// Reverses order of characters in a string
        /// </summary>
        [Pure, NotNull]
        public static string Reverse([NotNull] this string str)
        {
            GuardNull(str, nameof(str));

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
            GuardNull(str, nameof(str));
            GuardMin(count, 0, nameof(count));

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
            GuardMin(count, 0, nameof(count));

            if (count == 0)
                return string.Empty;

            return new string(c, count);
        }

        /// <summary>
        /// Truncates a string leaving only the given number of characters from the start of the string
        /// </summary>
        [Pure, NotNull]
        public static string Take([NotNull] this string str, int count)
        {
            GuardNull(str, nameof(str));
            GuardMin(count, 0, nameof(count));

            if (count == 0) return string.Empty;
            if (count >= str.Length) return str;
            return str.Substring(0, count);
        }

        /// <summary>
        /// Truncates a string dropping the given number of characters from the start of the string
        /// </summary>
        [Pure, NotNull]
        public static string Skip([NotNull] this string str, int count)
        {
            GuardNull(str, nameof(str));
            GuardMin(count, 0, nameof(count));

            if (count == 0) return str;
            if (count >= str.Length) return string.Empty;
            return str.Substring(count);
        }

        /// <summary>
        /// Truncates a string leaving only the given number of characters from the end of the string
        /// </summary>
        [Pure, NotNull]
        public static string TakeLast([NotNull] this string str, int count)
        {
            GuardNull(str, nameof(str));
            GuardMin(count, 0, nameof(count));

            if (count == 0) return string.Empty;
            if (count >= str.Length) return str;
            return Skip(str, str.Length - count);
        }

        /// <summary>
        /// Truncates a string dropping the given number of characters from the end of the string
        /// </summary>
        [Pure, NotNull]
        public static string SkipLast([NotNull] this string str, int count)
        {
            GuardNull(str, nameof(str));
            GuardMin(count, 0, nameof(count));

            if (count == 0) return str;
            if (count >= str.Length) return string.Empty;
            return Take(str, str.Length - count);
        }

        /// <summary>
        /// Removes all occurrences of the given substrings from a string
        /// </summary>
        [Pure, NotNull]
        public static string Except([NotNull] this string str, [NotNull] IEnumerable<string> substrings,
            StringComparison comparison = StringComparison.Ordinal)
        {
            GuardNull(str, nameof(str));
            GuardNull(substrings, nameof(substrings));

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
            GuardNull(str, nameof(str));
            GuardNull(characters, nameof(characters));

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
        public static string EnsureStartsWith([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

            return str.StartsWith(sub, comparison) ? str : sub + str;
        }

        /// <summary>
        /// Appends a string with the given string if it doesn't end with it already
        /// </summary>
        [Pure, NotNull]
        public static string EnsureEndsWith([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

            return str.EndsWith(sub, comparison) ? str : str + sub;
        }

        /// <summary>
        /// Retrieves a substring that ends at the position of first occurrence of the given other string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringUntil([NotNull] this string str, [NotNull] string sub,
            StringComparison comparison = StringComparison.Ordinal)
        {
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(str, nameof(str));
            GuardNull(sub, nameof(sub));

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
            GuardNull(enumerable, nameof(enumerable));

            return enumerable.Any(sub.EqualsInvariant);
        }

        /// <summary>
        /// Discards blank strings from a sequence
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<string> ExceptBlank([NotNull] this IEnumerable<string> enumerable)
        {
            GuardNull(enumerable, nameof(enumerable));

            return enumerable.Where(IsNotBlank);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] Split([NotNull] this string str, [NotNull] params string[] separators)
        {
            GuardNull(str, nameof(str));
            GuardNull(separators, nameof(separators));

            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits string using given separators, discarding empty entries
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] Split([NotNull] this string str, [NotNull] params char[] separators)
        {
            GuardNull(str, nameof(str));
            GuardNull(separators, nameof(separators));

            return str.Split(separators, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns a string formed by joining elements of a sequence using the given separator
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] string separator)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(separator, nameof(separator));

            return string.Join(separator, enumerable);
        }

        /// <summary>
        /// Returns a string formed by joining elements of a sequence using the given separator
        /// </summary>
        [Pure, NotNull]
        public static string JoinToString<T>([NotNull] this IEnumerable<T> enumerable, char separator)
            => JoinToString(enumerable, separator.ToString());

        /// <summary>
        /// Parses the string into an object of given type using a <see cref="ParseDelegate{T}"/> handler
        /// </summary>
        [Pure]
        public static T Parse<T>([NotNull] this string str, [NotNull] ParseDelegate<T> handler)
        {
            GuardNull(str, nameof(str));
            GuardNull(handler, nameof(handler));

            return handler(str);
        }

        /// <summary>
        /// Parses the string into an object of given type using a <see cref="TryParseDelegate{T}"/> handler or returns default value if unsuccessful
        /// </summary>
        [Pure]
        public static T ParseOrDefault<T>([CanBeNull] this string str, [NotNull] TryParseDelegate<T> handler,
            T defaultValue = default(T))
        {
            GuardNull(handler, nameof(handler));

            return handler(str, out T result) ? result : defaultValue;
        }
    }
}