using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Types;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Determines whether a sequence is not null and contains elements that satisfy a condition
        /// </summary>
        [Pure]
        [ContractAnnotation("enumerable:null => false")]
        public static bool NotNullAndAny<T>([CanBeNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(predicate, nameof(predicate));

            return enumerable != null && enumerable.Any(predicate);
        }

        /// <summary>
        /// Determines whether a sequence is not null and contains elements
        /// </summary>
        [Pure]
        [ContractAnnotation("enumerable:null => false")]
        public static bool NotNullAndAny<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Returns a random member of a sequence
        /// </summary>
        [Pure]
        public static T GetRandom<T>([NotNull] this IEnumerable<T> enumerable)
        {
            GuardNull(enumerable, nameof(enumerable));

            var list = enumerable as IList<T> ?? enumerable.ToArray();

            if (!list.Any()) throw new Exception("No items in enumerable");
            if (list.Count <= 1) return list.First();
            return list[SharedInstances.Random.Next(0, list.Count)];
        }

        /// <summary>
        /// Returns a random member of a sequence or default value if the sequence is empty
        /// </summary>
        [Pure]
        public static T GetRandomOrDefault<T>([NotNull] this IEnumerable<T> enumerable, T defaultValue = default(T))
        {
            GuardNull(enumerable, nameof(enumerable));

            var list = enumerable as IList<T> ?? enumerable.ToArray();
            if (!list.Any()) return defaultValue;
            return GetRandom(list);
        }

        /// <summary>
        /// Returns an empty sequence if the given sequence is null, otherwise returns given sequence
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> EmptyIfNull<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            return enumerable ?? Enumerable.Empty<T>();
        }

        /// <summary>
        /// Returns distinct elements from a sequence based on a key
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Distinct<T, TKey>([NotNull] this IEnumerable<T> enumerable,
            [NotNull] Func<T, TKey> keySelector, [NotNull] IEqualityComparer<TKey> keyComparer)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(keySelector, nameof(keySelector));
            GuardNull(keyComparer, nameof(keyComparer));

            bool Compare(T x, T y) => keyComparer.Equals(keySelector(x), keySelector(y));
            int GetHash(T obj) => keySelector(obj).GetHashCode();
            return enumerable.Distinct(new DelegateEqualityComparer<T>(Compare, GetHash));
        }

        /// <summary>
        /// Returns distinct elements from a sequence based on a key
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<TSource> Distinct<TSource, TKey>([NotNull] this IEnumerable<TSource> enumerable,
                [NotNull] Func<TSource, TKey> keySelector)
            => Distinct(enumerable, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Calculates hash code of a sequence based on the hash codes of each individual elements
        /// </summary>
        [Pure]
        public static int SequenceHashCode<T>([NotNull] this IEnumerable<T> enumerable, bool ignoreOrder = false)
        {
            GuardNull(enumerable, nameof(enumerable));

            var hashes = enumerable.Select(i => i?.GetHashCode() ?? 0);
            if (ignoreOrder)
                hashes = hashes.OrderBy(i => i);

            var result = 19;
            foreach (var hash in hashes)
            {
                unchecked
                {
                    result = result*31 + hash;
                }
            }

            return result;
        }

        /// <summary>
        /// Discards elements from a sequence that are equal to given
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Except<T>([NotNull] this IEnumerable<T> enumerable, T value, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(comparer, nameof(comparer));

            return enumerable.Where(i => !comparer.Equals(i, value));
        }

        /// <summary>
        /// Discards elements from a sequence that are equal to given
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Except<T>([NotNull] this IEnumerable<T> enumerable, T value)
            => Except(enumerable, value, EqualityComparer<T>.Default);

        /// <summary>
        /// Discards default values from a sequence
        /// </summary>
        [Pure, NotNull, ItemNotNull]
        public static IEnumerable<T> ExceptDefault<T>([NotNull] this IEnumerable<T> enumerable)
            => Except(enumerable, default(T));

        /// <summary>
        /// Returns a specified number of contiguous elements from the end of a sequence
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> TakeLast<T>([NotNull] this IEnumerable<T> enumerable, int count)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardMin(count, 0, nameof(count));

            if (count == 0)
                return Enumerable.Empty<T>();

            var asCol = enumerable as ICollection<T> ?? enumerable.ToArray();
            var skip = asCol.Count - count;
            if (skip < 0) skip = 0;
            return asCol.Skip(skip);
        }

        /// <summary>
        /// Bypasses a specified number of elements at the end of a sequence and returns the remaining elements
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLast<T>([NotNull] this IEnumerable<T> enumerable, int count)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardMin(count, 0, nameof(count));

            if (count == 0)
                return enumerable;

            var asCol = enumerable as ICollection<T> ?? enumerable.ToArray();
            var take = asCol.Count - count;
            if (take < 0) take = 0;
            return asCol.Take(take);
        }

        /// <summary>
        /// Returns elements from the end of the sequence as long as a specified condition is true
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> TakeLastWhile<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(predicate, nameof(predicate));

            return enumerable.Reverse().TakeWhile(predicate).Reverse();
        }

        /// <summary>
        /// Bypasses elements from the end of the sequence as long as a specified condition is true
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLastWhile<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(predicate, nameof(predicate));

            return enumerable.Reverse().SkipWhile(predicate).Reverse();
        }

        /// <summary>
        /// Invokes a delegate on each member of a sequence
        /// </summary>
        public static void ForEach<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> action)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(action, nameof(action));

            foreach (var obj in enumerable)
                action(obj);
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(comparer, nameof(comparer));

            return new HashSet<T>(enumerable, comparer);
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> enumerable)
            => ToHashSet(enumerable, EqualityComparer<T>.Default);

        /// <summary>
        /// Adds a new item to a collection if it wasn't there already
        /// </summary>
        /// <returns>True if it was added, false if it was already there</returns>
        public static bool AddIfDistinct<T>([NotNull] this ICollection<T> collection, T obj, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(collection, nameof(collection));
            GuardNull(comparer, nameof(comparer));

            var isDistinct = !collection.Contains(obj, comparer);
            if (isDistinct) collection.Add(obj);
            return isDistinct;
        }

        /// <summary>
        /// Adds a new item to a collection if it wasn't there already
        /// </summary>
        /// <returns>True if it was added, false if it was already there</returns>
        public static bool AddIfDistinct<T>([NotNull] this ICollection<T> collection, T obj)
            => AddIfDistinct(collection, obj, EqualityComparer<T>.Default);

        /// <summary>
        /// Searches for an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> enumerable, T element, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(comparer, nameof(comparer));

            var i = 0;
            foreach (var item in enumerable)
            {
                if (comparer.Equals(item, element))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> enumerable, T element)
            => IndexOf(enumerable, element, EqualityComparer<T>.Default);

        /// <summary>
        /// Searches for an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(predicate, nameof(predicate));

            var i = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> enumerable, T element, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(comparer, nameof(comparer));

            var index = -1;
            var i = 0;
            foreach (var item in enumerable)
            {
                if (comparer.Equals(item, element))
                    index = i;
                i++;
            }
            return index;
        }

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> enumerable, T element)
            => LastIndexOf(enumerable, element, EqualityComparer<T>.Default);

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(enumerable, nameof(enumerable));
            GuardNull(predicate, nameof(predicate));

            var index = -1;
            var i = 0;
            foreach (var item in enumerable)
            {
                if (predicate(item))
                    index = i;
                i++;
            }
            return index;
        }

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, T element, [NotNull] IEqualityComparer<T> comparer)
        {
            GuardNull(list, nameof(list));
            GuardNull(comparer, nameof(comparer));

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (comparer.Equals(list[i], element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, T element)
            => LastIndexOf(list, element, EqualityComparer<T>.Default);

        /// <summary>
        /// Searches for the last occurrence of an item and returns its index
        /// <returns>Item index if found, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, [NotNull] Func<T, bool> predicate)
        {
            GuardNull(list, nameof(list));
            GuardNull(predicate, nameof(predicate));

            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last element in a sequence
        /// <returns>Index if there are any items, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndex<T>([NotNull] this IEnumerable<T> enumerable)
        {
            GuardNull(enumerable, nameof(enumerable));

            return enumerable.Count() - 1;
        }

        /// <summary>
        /// Returns the index of the last element in an array for the given dimension
        /// <returns>Index if there are any items and the dimension exists, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndex([NotNull] this Array array, int dimension = 0)
        {
            GuardNull(array, nameof(array));
            GuardMin(dimension, 0, nameof(dimension));

            if (dimension > array.Rank - 1)
                return -1;

            return array.GetUpperBound(dimension);
        }

        /// <summary>
        /// Returns an item that corresponds to the given key or default if key doesn't exist
        /// </summary>
        [Pure]
        public static TValue GetOrDefault<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dic, TKey key,
            TValue defaultValue = default(TValue))
        {
            GuardNull(dic, nameof(dic));

            return dic.TryGetValue(key, out TValue result) ? result : defaultValue;
        }

        /// <summary>
        /// Sets allocated items in a list to the given value
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value, int startIndex, int count)
        {
            GuardNull(list, nameof(list));
            GuardRange(startIndex, 0, list.Count - 1, nameof(startIndex));
            GuardRange(count, 0, list.Count - startIndex, nameof(count));

            if (count == 0)
                return;

            for (var i = startIndex; i < startIndex + count; i++)
                list[i] = value;
        }

        /// <summary>
        /// Sets allocated items in a list to the given value
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value, int startIndex)
            => Fill(list, value, startIndex, list.Count - startIndex);

        /// <summary>
        /// Sets allocated items in a list to the given value
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value)
            => Fill(list, value, 0, list.Count);

        /// <summary>
        /// Makes sure that a list has no more items than specified by removing other items
        /// </summary>
        public static void EnsureMaxCount<T>([NotNull] this IList<T> list, int count, EnsureMaxCountMode mode = EnsureMaxCountMode.DeleteFirst)
        {
            GuardNull(list, nameof(list));
            GuardMin(count, 0, nameof(count));

            // Set up cleaner
            Action cleaner;
            switch (mode)
            {
                case EnsureMaxCountMode.DeleteFirst:
                    cleaner = () => list.RemoveAt(0);
                    break;
                case EnsureMaxCountMode.DeleteLast:
                    cleaner = () => list.RemoveAt(list.Count - 1);
                    break;
                case EnsureMaxCountMode.DeleteRandom:
                    cleaner = () => list.RemoveAt(SharedInstances.Random.Next(0, list.Count));
                    break;
                case EnsureMaxCountMode.DeleteAll:
                    cleaner = list.Clear;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }

            // Run cleaner
            while (list.Count > count && list.Count > 0)
                cleaner();
        }

        /// <summary>
        /// Sets or adds value in a dictionary that corresponds to the given key
        /// <returns>True if the value was set to an existing key, false if a new key/value pair was added</returns>
        /// </summary>
        public static bool SetOrAdd<TKey, TValue>([NotNull] this IDictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            GuardNull(dic, nameof(dic));

            if (dic.ContainsKey(key))
            {
                dic[key] = value;
                return true;
            }
            dic.Add(key, value);
            return false;
        }
    }
}