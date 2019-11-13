using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ISet{T}" />.
    /// </summary>
    public static class SetExtensions
    {
        /// <summary>
        /// Creates a hashset from given sequence.
        /// </summary>
        [return: NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> source, [NotNull] IEqualityComparer<T> comparer)
        {
            return new HashSet<T>(source, comparer);
        }

        /// <summary>
        /// Creates a hashset from given sequence.
        /// </summary>
        [return: NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> source) => source.ToHashSet(EqualityComparer<T>.Default);

        /// <summary>
        /// Adds items to the set and returns the number of items that were successfully added.
        /// </summary>
        public static int AddRange<T>([NotNull] this ISet<T> source, [NotNull] IEnumerable<T> items)
        {
            return items.Count(source.Add);
        }
    }
}