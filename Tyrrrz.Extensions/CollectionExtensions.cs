using System.Collections.Generic;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="ICollection{T}" />.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds multiple items to the collection.
        /// </summary>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }
    }
}