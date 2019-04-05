using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

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
        [Pure]
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> variants,
            [NotNull] IEqualityComparer<T> comparer)
        {
            variants.GuardNotNull(nameof(variants));
            comparer.GuardNotNull(nameof(comparer));

            return variants.Contains(obj, comparer);
        }

        /// <summary>
        /// Determines whether an object is equal to any of the elements in a sequence.
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, [NotNull] IEnumerable<T> variants)
            => IsEither(obj, variants, EqualityComparer<T>.Default);

        /// <summary>
        /// Determines whether the object is equal to any of the parameters.
        /// </summary>
        [Pure]
        public static bool IsEither<T>(this T obj, params T[] variants) => IsEither(obj, (IEnumerable<T>) variants);
    }
}