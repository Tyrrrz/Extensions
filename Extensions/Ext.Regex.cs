using System;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Matches input with a regular expression and returns the match value if successful or null if not
        /// </summary>
        [Pure]
        public static string MatchOrNull([NotNull] this Regex regex, [NotNull] string input)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var match = regex.Match(input);
            if (match.Success)
                return match.Value;
            return null;
        }

        /// <summary>
        /// Matches input with a regular expression and returns the match value of selected group if successful or null if not
        /// </summary>
        [Pure]
        public static string MatchOrNull([NotNull] this Regex regex, [NotNull] string input, int group)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (group < 0)
                throw new ArgumentOutOfRangeException(nameof(group), "Cannot be negative");

            var match = regex.Match(input);
            if (match.Success)
                return match.Groups[group].Value;
            return null;
        }

        /// <summary>
        /// Matches input with a regular expression and returns the match value of selected group if successful or null if not
        /// </summary>
        [Pure]
        public static string MatchOrNull([NotNull] this Regex regex, [NotNull] string input, [NotNull] string group)
        {
            if (regex == null)
                throw new ArgumentNullException(nameof(regex));
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            if (group == null)
                throw new ArgumentNullException(nameof(group));

            var match = regex.Match(input);
            if (match.Success)
                return match.Groups[group].Value;
            return null;
        }
    }
}