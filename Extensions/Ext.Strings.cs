using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Gets the array of non-mutual chars between two strings
        /// </summary>
        [Pure, NotNull]
        public static char[] StringDifference([NotNull] this string source, [NotNull] string other)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            var diff1 = source.Where(c => !other.Contains(c));
            var diff2 = other.Where(c => !source.Contains(c));
            var union = diff1.Union(diff2);
            return union.ToArray();
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

            return new string(str.Reverse<char>().ToArray());
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
            if (count == 1)
                return str;

            var sb = new StringBuilder(str, count);
            for (int i = 2; i <= count; i++)
                sb.Append(str);

            return sb.ToString();
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
            if (charCount >= str.Length)
                return string.Empty;
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

            string match;
            int pos = IndexOfAny(str, subStrings, 0, StringComparison.Ordinal, out match);
            while (pos >= 0)
            {
                str = str.Remove(pos, match.Length);
                pos = IndexOfAny(str, subStrings, 0, StringComparison.Ordinal, out match);
            }
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
        public static string SubstringUntil([NotNull] this string str, [NotNull] string sub, StringComparison comparison = StringComparison.Ordinal)
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
        /// Returns substring of a string after the occurence of the other string
        /// If the other string is not found, returns full string
        /// </summary>
        [Pure, NotNull]
        public static string SubstringAfter([NotNull] this string str, [NotNull] string sub, StringComparison comparison = StringComparison.Ordinal)
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
        /// Converts byte array to string
        /// </summary>
        [Pure]
        public static string GetString(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }

        /// <summary>
        /// Converts byte array to string using UTF8 encoding
        /// </summary>
        [Pure]
        public static string GetString(this byte[] data)
        {
            return GetString(data, Encoding.UTF8);
        }

        /// <summary>
        /// Converts string to byte array
        /// </summary>
        [Pure]
        public static byte[] GetBytes(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// Converts string to byte array using UTF8 encoding
        /// </summary>
        [Pure]
        public static byte[] GetBytes(this string str)
        {
            return GetBytes(str, Encoding.UTF8);
        }

        /// <summary>
        /// Converts an array of bytes to a base64 string
        /// </summary>
        [Pure]
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a base64 string to a byte array
        /// </summary>
        [Pure]
        public static byte[] FromBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }

        /// <summary>
        /// Split string into substrings using separator strings.
        /// Separators are also included.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] SplitInclusive([NotNull] this string str, StringComparison comparison = StringComparison.Ordinal, params string[] separators)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            var result = new List<string>();

            var start = 0;
            int i;
            do
            {
                // Find a separator and figure out which one
                string separator;
                i = IndexOfAny(str, separators, start, comparison, out separator);

                // Extract substring before separator
                string sub = i >= 0
                    ? str.Substring(start, i - start) // separator
                    : str.Substring(start); // no separator

                // Add substring to result
                if (!string.IsNullOrEmpty(sub))
                    result.Add(sub);

                // If a separator was found - add separator to result
                if (!string.IsNullOrEmpty(separator))
                    result.Add(separator);

                // Increment start
                if (i >= 0 && !string.IsNullOrEmpty(separator))
                    start = i + separator.Length;
            } while (i >= 0);

            // Returns array to be consistent with .NET's .Split() method
            return result.ToArray();
        }

        /// <summary>
        /// Split string into substrings using separator string.
        /// Empty items are removed and existing are trimmed.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] SplitTrim([NotNull] this string str, [NotNull] string separator, params char[] trimChars)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (separator == null)
                throw new ArgumentNullException(nameof(separator));

            return str
                .Split(new[] {separator}, StringSplitOptions.RemoveEmptyEntries)
                .Select(sub => sub.Trim(trimChars))
                .ToArray();
        }

        /// <summary>
        /// Split string into substrings using separator string.
        /// Empty items are removed and existing are trimmed.
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static string[] SplitTrim([NotNull] this string str, [NotNull] string separator)
        {
            return str.SplitTrim(separator, ' ');
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
        /// Strips the longest common string for the given enumerable of string, which has it's origin at string start
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
        /// Strips the longest common string for the given enumerable of string, which has it's origin at string end
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
        /// Returns the same enumerable, with all its elements trimmed
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static IEnumerable<string> TrimElements([NotNull] this IEnumerable<string> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Select(str => str.Trim());
        }

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
        public static bool ContainsWord([NotNull] this string str, [NotNull] string word,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (word == null)
                throw new ArgumentNullException(nameof(word));

            str = str.Trim();
            word = word.Trim();

            if (str.Equals(word, comparison)) return true;

            // Find the position of word
            var wordBoundaries = new[] { '\n', '\r', ' ' };
            foreach (int pos in str.IndicesOf(word, 0, comparison))
            {
                bool leftCheck = false, rightCheck = false;

                // See if the word starts at the start or ends at the end
                if (pos == 0) leftCheck = true;
                if (pos + word.Length == str.Length) rightCheck = true;

                // Check the bounding characters
                if (!leftCheck)
                {
                    char leftChar = str[pos - 1];
                    if (wordBoundaries.Contains(leftChar)) leftCheck = true;
                }
                if (!rightCheck)
                {
                    char rightChar = str[pos + word.Length];
                    if (wordBoundaries.Contains(rightChar)) rightCheck = true;
                }

                if (leftCheck && rightCheck) return true;
            }

            return false;
        }

        /// <summary>
        /// Compute Levenshtein distance (cost) between two strings
        /// </summary>
        [Pure]
        public static int LevenshteinDistance([NotNull] this string s1, [NotNull] string s2)
        {
            if (s1 == null)
                throw new ArgumentNullException(nameof(s1));
            if (s2 == null)
                throw new ArgumentNullException(nameof(s2));

            int len1 = s1.Length;
            int len2 = s2.Length;

            if (len1 == 0) return len2;
            if (len2 == 0) return len1;

            int cost = s1.Last() == s2.Last() ? 0 : 1;

            var i = new[]
            {
                LevenshteinDistance(s1.SkipLast(1), s2) + 1,
                LevenshteinDistance(s1, s2.SkipLast(1)) + 1,
                LevenshteinDistance(s1.SkipLast(1), s2.SkipLast(1)) + cost
            };
            return i.Min();
        }

        /// <summary>
        /// Get the number of occurrences of a substring inside a string
        /// </summary>
        [Pure]
        public static int GetNumberOfOccurences([NotNull] this string str, [NotNull] string sub, int start = 0,
            StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start), "Cannot be negative");

            int result = 0;
            int index = str.IndexOf(sub, start, comparison);
            while (index >= 0)
            {
                result++;
                index = str.IndexOf(sub, index + sub.Length + 1, comparison);
            }
            return result;
        }

        /// <summary>
        /// Returns the first occurence of any substrings in enumerable
        /// </summary>
        [Pure]
        public static int IndexOfAny([NotNull] this string str, [NotNull] IEnumerable<string> subStrings, int start,
            StringComparison comparison, [NotNull] out string match)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (subStrings == null)
                throw new ArgumentNullException(nameof(subStrings));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start), "Cannot be negative");

            int index = -1;
            match = string.Empty;

            foreach (string sub in subStrings)
            {
                int curIndex = str.IndexOf(sub, start, comparison);
                if (curIndex < 0 || curIndex >= index) continue;

                match = str.Substring(curIndex, sub.Length);
                if (curIndex == 0) return 0;
                index = curIndex;
            }

            return index;
        }

        /// <summary>
        /// Returns the first occurence of any substrings in enumerable
        /// </summary>
        [Pure]
        public static int IndexOfAny([NotNull] this string str, [NotNull] IEnumerable<string> subStrings, int start = 0,
            StringComparison comparison = StringComparison.Ordinal)
        {
            string match;
            return IndexOfAny(str, subStrings, start, comparison, out match);
        }

        /// <summary>
        /// Returns all position indices of given substring inside given string
        /// </summary>
        [Pure, NotNull]
        public static int[] IndicesOf([NotNull] this string str, [NotNull] string sub, int start = 0, StringComparison comparison = StringComparison.Ordinal)
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));
            if (sub == null)
                throw new ArgumentNullException(nameof(sub));
            if (start < 0)
                throw new ArgumentOutOfRangeException(nameof(start), "Cannot be negative");

            var result = new List<int>();
            int index = str.IndexOf(sub, start, comparison);
            while (index >= 0)
            {
                result.Add(index);
                index = str.IndexOf(sub, index + sub.Length, comparison);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Converts a string to URI
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return new UriBuilder(uri).Uri;
        }

        /// <summary>
        /// Converts a string to relative uri, with a given base uri
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] string baseUri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(ToUri(baseUri), new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        /// Converts a string to relative uri, with a given base uri
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] Uri baseUri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(baseUri, new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        /// Returns URL encoded version of the given string
        /// </summary>
        [Pure, NotNull]
        public static string UrlEncode([NotNull] this string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

#if Net45
            return WebUtility.UrlEncode(data);
#else
            return Uri.EscapeDataString(data);
#endif
        }

        /// <summary>
        /// Returns URL decoded version of the given string
        /// </summary>
        [Pure, NotNull]
        public static string UrlDecode([NotNull] this string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

#if Net45
            return WebUtility.UrlDecode(data);
#else
            return Uri.UnescapeDataString(data);
#endif
        }

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