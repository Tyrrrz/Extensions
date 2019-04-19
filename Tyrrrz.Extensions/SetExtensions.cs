using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

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
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> source, [NotNull] IEqualityComparer<T> comparer)
        {
            source.GuardNotNull(nameof(source));
            comparer.GuardNotNull(nameof(comparer));

            return new HashSet<T>(source, comparer);
        }

        /// <summary>
        /// Creates a hashset from given sequence.
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> source) => source.ToHashSet(EqualityComparer<T>.Default);

        /// <summary>
        /// Adds items to the set and returns the number of items that were successfully added.
        /// </summary>
        public static int AddRange<T>([NotNull] this ISet<T> source, [NotNull] IEnumerable<T> items)
        {
            source.GuardNotNull(nameof(source));
            items.GuardNotNull(nameof(items));

            return items.Count(source.Add);
        }
    }
}