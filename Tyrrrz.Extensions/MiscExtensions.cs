using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Misc extensions.
    /// </summary>
    public static class MiscExtensions
    {
        /// <summary>
        /// Determines whether an object is equal to any of the elements in a sequence.
        /// </summary>
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> variants,
            [NotNull] IEqualityComparer<T> comparer)
        {
            return variants.Contains(obj, comparer);
        }

        /// <summary>
        /// Determines whether an object is equal to any of the elements in a sequence.
        /// </summary>
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> variants) =>
            IsEither(obj, variants, EqualityComparer<T>.Default);

        /// <summary>
        /// Determines whether the object is equal to any of the parameters.
        /// </summary>
        public static bool IsEither<T>(this T obj, params T[] variants) => IsEither(obj, (IEnumerable<T>) variants);
    }
}