using System.Collections.Generic;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IDictionary{TKey, TValue}" /> and <see cref="IReadOnlyDictionary{TKey, TValue}" />.
    /// </summary>
    public static class DictionaryExtensions
    {
#if !NETSTANDARD2_1
        /// <summary>
        /// Returns a value that corresponds to the given key or default if the key doesn't exist.
        /// </summary>
        [Pure]
        public static TValue GetValueOrDefault<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            dictionary.GuardNotNull(nameof(dictionary));
            return dictionary.TryGetValue(key, out var result) ? result : default;
        }
#endif
    }
}