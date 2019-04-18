using System.Collections.Generic;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see href="IDictionary{T}" /> and <see href="IReadOnlyDictionary{T}" />.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Returns a value that corresponds to the given key or default if the key doesn't exist.
        /// </summary>
        [Pure]
        public static TValue GetValueOrDefault<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.GuardNotNull(nameof(dictionary));
            return dictionary.TryGetValue(key, out var result) ? result : default;
        }
    }
}