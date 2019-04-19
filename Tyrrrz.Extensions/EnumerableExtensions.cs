using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable{T}" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Indicates whether the sequence is null or an empty sequence.
        /// </summary>
        [Pure]
        [ContractAnnotation("source:null => true")]
        public static bool IsNullOrEmpty<T>([CanBeNull] this IEnumerable<T> source) => source == null || !source.Any();

        /// <summary>
        /// Returns an empty sequence if the given sequence is null, otherwise returns given sequence.
        /// </summary>
        [Pure, NotNull]
        [ContractAnnotation("source:null => notnull")]
        public static IEnumerable<T> EmptyIfNull<T>([CanBeNull] this IEnumerable<T> source) => source ?? Enumerable.Empty<T>();

        /// <summary>
        /// Calculates aggregated hash code of all elements in a sequence.
        /// </summary>
        [Pure]
        public static int GetSequenceHashCode<T>([NotNull] this IEnumerable<T> source, bool ignoreOrder = false)
        {
            source.GuardNotNull(nameof(source));

            // Calculate all hashes
            var hashes = source.Select(i => i?.GetHashCode() ?? 0);

            // If the order is irrelevant - order the list to ensure the order is always the same
            if (ignoreOrder)
                hashes = hashes.OrderBy(i => i);

            // Aggregate individual hashes
            var result = 19;
            foreach (var hash in hashes)
            {
                unchecked
                {
                    result = result * 31 + hash;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns a random element in a sequence.
        /// </summary>
        [Pure]
        public static T Random<T>([NotNull] this IEnumerable<T> source)
        {
            source.GuardNotNull(nameof(source));

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If there are no elements - throw
            if (!asReadOnlyList.Any())
                throw new InvalidOperationException("Sequence contains no elements.");

            return asReadOnlyList[RandomEx.GetInt(0, asReadOnlyList.Count)];
        }

        /// <summary>
        /// Returns a random element in a sequence or default if there are no elements.
        /// </summary>
        [Pure, CanBeNull]
        public static T RandomOrDefault<T>([NotNull] this IEnumerable<T> source)
        {
            source.GuardNotNull(nameof(source));

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If there are no elements - return default
            if (!asReadOnlyList.Any())
                return default;

            return asReadOnlyList[RandomEx.GetInt(0, asReadOnlyList.Count)];
        }

        /// <summary>
        /// Returns elements with distinct keys.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Distinct<T, TKey>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, TKey> keySelector, [NotNull] IEqualityComparer<TKey> keyComparer)
        {
            source.GuardNotNull(nameof(source));
            keySelector.GuardNotNull(nameof(keySelector));
            keyComparer.GuardNotNull(nameof(keyComparer));

            // Use a hashset to maintain uniqueness of keys
            var keyHashSet = new HashSet<TKey>(keyComparer);
            foreach (var element in source)
            {
                if (keyHashSet.Add(keySelector(element)))
                    yield return element;
            }
        }

        /// <summary>
        /// Returns elements with distinct keys.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Distinct<T, TKey>([NotNull] this IEnumerable<T> source, [NotNull] Func<T, TKey> keySelector) =>
            source.Distinct(keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Discards elements from a sequence that are equal to given value.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Except<T>([NotNull] this IEnumerable<T> source, T value,
            [NotNull] IEqualityComparer<T> comparer)
        {
            source.GuardNotNull(nameof(source));
            comparer.GuardNotNull(nameof(comparer));

            return source.Where(i => !comparer.Equals(i, value));
        }

        /// <summary>
        /// Discards elements from a sequence that are equal to given value.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Except<T>([NotNull] this IEnumerable<T> source, T value) => source.Except(value, EqualityComparer<T>.Default);

        /// <summary>
        /// Discards default values from a sequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> ExceptDefault<T>([NotNull] this IEnumerable<T> source) => source.Except(default);

        /// <summary>
        /// Slices a sequence into a subsequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Slice<T>([NotNull] this IEnumerable<T> source, int startAt, int count)
        {
            source.GuardNotNull(nameof(source));
            startAt.GuardNotNegative(nameof(startAt));
            count.GuardNotNegative(nameof(count));

            // If count is zero - return empty
            if (count == 0)
                yield break;

            var i = 0;
            foreach (var element in source)
            {
                // If the index is within range - yield element
                if (i >= startAt && i <= startAt + count - 1)
                    yield return element;

                // If the index is past bounds - break
                if (i >= startAt + count)
                    yield break;

                // Increment index
                i++;
            }
        }

        /// <summary>
        /// Returns a specified number of contiguous elements at the end of a sequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> TakeLast<T>([NotNull] this IEnumerable<T> source, int count)
        {
            source.GuardNotNull(nameof(source));
            count.GuardNotNegative(nameof(count));

            // If count is 0 - return empty
            if (count == 0)
                return Enumerable.Empty<T>();

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If count is greater than element count - return source
            if (count >= asReadOnlyList.Count)
                return asReadOnlyList;

            // Otherwise - slice
            return asReadOnlyList.Slice(asReadOnlyList.Count - count, count);
        }

        /// <summary>
        /// Bypasses a specified number of contiguous elements at the end of a sequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLast<T>([NotNull] this IEnumerable<T> source, int count)
        {
            source.GuardNotNull(nameof(source));
            count.GuardNotNegative(nameof(count));

            // If count is 0 - return source
            if (count == 0)
                return source;

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // If count is greater than element count - return empty
            if (count >= asReadOnlyList.Count)
                return Enumerable.Empty<T>();

            // Otherwise - slice
            return asReadOnlyList.Slice(0, asReadOnlyList.Count - count);
        }

        /// <summary>
        /// Returns elements from the end of a sequence as long as a specified condition is true.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> TakeLastWhile<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, bool> predicate)
        {
            source.GuardNotNull(nameof(source));
            predicate.GuardNotNull(nameof(predicate));

            return source.Reverse().TakeWhile(predicate).Reverse();
        }

        /// <summary>
        /// Bypasses elements from the end of a sequence as long as a specified condition is true.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLastWhile<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, bool> predicate)
        {
            source.GuardNotNull(nameof(source));
            predicate.GuardNotNull(nameof(predicate));

            return source.Reverse().SkipWhile(predicate).Reverse();
        }

        /// <summary>
        /// Returns index of the first element in a sequence that matches the predicate.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> source, [NotNull] Func<T, bool> predicate)
        {
            source.GuardNotNull(nameof(source));
            predicate.GuardNotNull(nameof(predicate));

            var i = 0;
            foreach (var element in source)
            {
                // If matches - return index
                if (predicate(element))
                    return i;

                // Increment index
                i++;
            }

            // If nothing found - return -1
            return -1;
        }

        /// <summary>
        /// Returns index of the first element in a sequence equal to given value.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> source, T element,
            [NotNull] IEqualityComparer<T> comparer)
        {
            source.GuardNotNull(nameof(source));
            comparer.GuardNotNull(nameof(comparer));

            return source.IndexOf(i => comparer.Equals(i, element));
        }

        /// <summary>
        /// Returns index of the first element in a sequence equal to given value.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> source, T element) =>
            source.IndexOf(element, EqualityComparer<T>.Default);

        /// <summary>
        /// Returns index of the last element in a sequence that matches the predicate.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> source, [NotNull] Func<T, bool> predicate)
        {
            source.GuardNotNull(nameof(source));
            predicate.GuardNotNull(nameof(predicate));

            // Buffer all elements
            var asReadOnlyList = source as IReadOnlyList<T> ?? source.ToArray();

            // Loop in reverse
            for (var i = asReadOnlyList.Count - 1; i >= 0; i--)
            {
                // If matches - return index
                if (predicate(asReadOnlyList[i]))
                    return i;
            }

            // If nothing found - return -1
            return -1;
        }

        /// <summary>
        /// Returns index of the last element in a sequence equal to given value.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> source, T element,
            [NotNull] IEqualityComparer<T> comparer)
        {
            source.GuardNotNull(nameof(source));
            comparer.GuardNotNull(nameof(comparer));

            return source.LastIndexOf(i => comparer.Equals(i, element));
        }

        /// <summary>
        /// Returns index of the last element in a sequence equal to given value.
        /// If there is no such element - returns -1;
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> source, T element) =>
            source.LastIndexOf(element, EqualityComparer<T>.Default);

        /// <summary>
        /// Groups contiguous elements into a list based on a predicate.
        /// The predicate decides whether the next element should be added to the current group.
        /// If the predicate fails, the current group is closed and a new one, containing this element, is created.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<IReadOnlyList<T>> GroupContiguous<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<IReadOnlyList<T>, T, bool> groupPredicate)
        {
            source.GuardNotNull(nameof(source));
            groupPredicate.GuardNotNull(nameof(groupPredicate));

            // Create buffer
            var buffer = new List<T>();

            // Enumerate source
            foreach (var element in source)
            {
                // If buffer is not empty and group predicate failed - yield and reset buffer
                if (buffer.Any() && !groupPredicate(buffer, element))
                {
                    yield return buffer;
                    buffer = new List<T>(); // new instance to reset reference
                }

                // Add element to buffer
                buffer.Add(element);
            }

            // If buffer still has something after the source has been enumerated - yield
            if (buffer.Any())
                yield return buffer;
        }

        /// <summary>
        /// Groups contiguous elements into a list based on a predicate.
        /// The predicate decides whether the next element should be added to the current group.
        /// If the predicate fails, the current group is closed and a new one, containing this element, is created.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<IReadOnlyList<T>> GroupContiguous<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<IReadOnlyList<T>, bool> groupPredicate)
        {
            source.GuardNotNull(nameof(source));
            groupPredicate.GuardNotNull(nameof(groupPredicate));

            return source.GroupContiguous((buffer, _) => groupPredicate(buffer));
        }
    }
}