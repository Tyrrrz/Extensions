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
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var list = enumerable as IList<T> ?? enumerable.ToArray();
            if (!list.Any()) throw new ArgumentException("No items in enumerable", nameof(enumerable));
            if (list.Count <= 1) return list.First();
            return list[SharedInstances.Random.Next(0, list.Count)];
        }

        /// <summary>
        /// Returns a random member of a sequence or default value if the sequence is empty
        /// </summary>
        [Pure]
        public static T GetRandomOrDefault<T>([NotNull] this IEnumerable<T> enumerable, T defaultValue = default(T))
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

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
        /// Returns distinct elements from a sequence by using a selector delegate to compare values
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Distinct<T, TKey>([NotNull] this IEnumerable<T> enumerable,
            [NotNull] Func<T, TKey> keySelector, [NotNull] IEqualityComparer<TKey> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            Func<T, T, bool> compareDel = (x, y) => comparer.Equals(keySelector(x), keySelector(y));
            Func<T, int> hashDel = obj => keySelector(obj).GetHashCode();
            return enumerable.Distinct(new DelegateEqualityComparer<T>(compareDel, hashDel));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a selector delegate to compare values
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<TSource> Distinct<TSource, TKey>([NotNull] this IEnumerable<TSource> enumerable,
                [NotNull] Func<TSource, TKey> keySelector)
            => Distinct(enumerable, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Flattens the given sequences into one sequence
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SelectMany<T>([NotNull] this IEnumerable<IEnumerable<T>> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.SelectMany(i => i);
        }

        /// <summary>
        /// Calculates hash code of a sequence based on the hash codes of each individual elements
        /// </summary>
        [Pure]
        public static int SequenceHashCode<T>([NotNull] this IEnumerable<T> enumerable, bool ignoreOrder = false)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var hashes = enumerable.Select(i => i?.GetHashCode() ?? 0);
            if (ignoreOrder)
                hashes = hashes.OrderBy(i => i);

            int result = 19;
            foreach (int hash in hashes)
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (count == 0)
                return Enumerable.Empty<T>();

            var asCol = enumerable as ICollection<T> ?? enumerable.ToArray();
            int skip = asCol.Count - count;
            if (skip < 0) skip = 0;
            return asCol.Skip(skip);
        }

        /// <summary>
        /// Bypasses a specified number of elements at the end of a sequence and returns the remaining elements
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLast<T>([NotNull] this IEnumerable<T> enumerable, int count)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (count == 0)
                return enumerable;

            var asCol = enumerable as ICollection<T> ?? enumerable.ToArray();
            int take = asCol.Count - count;
            if (take < 0) take = 0;
            return asCol.Take(take);
        }

        /// <summary>
        /// Returns elements from the end of the sequence as long as a specified condition is true
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> TakeLastWhile<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return enumerable.Reverse().TakeWhile(predicate).Reverse();
        }

        /// <summary>
        /// Bypasses elements from the end of the sequence as long as a specified condition is true
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SkipLastWhile<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, bool> predicate)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            return enumerable.Reverse().SkipWhile(predicate).Reverse();
        }

        /// <summary>
        /// Invokes a delegate on every member of a sequence
        /// </summary>
        public static void ForEach<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            foreach (var obj in enumerable)
                action(obj);
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] IEqualityComparer<T> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            return new HashSet<T>(enumerable, comparer);
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> enumerable)
            => ToHashSet(enumerable, EqualityComparer<T>.Default);

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence, with the given selector determining distinct elements
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T, TKey>([NotNull] this IEnumerable<T> enumerable,
            [NotNull] Func<T, TKey> keySelector, [NotNull] IEqualityComparer<TKey> comparer)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            Func<T, T, bool> compareDel = (x, y) => comparer.Equals(keySelector(x), keySelector(y));
            Func<T, int> hashDel = obj => keySelector(obj).GetHashCode();
            return new HashSet<T>(enumerable, new DelegateEqualityComparer<T>(compareDel, hashDel));
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> by copying elements from a sequence, with the given selector to determine distinct elements
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T, TKey>([NotNull] this IEnumerable<T> enumerable,
                [NotNull] Func<T, TKey> keySelector)
            => ToHashSet(enumerable, keySelector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// Adds a new item to a collection if it wasn't there already
        /// </summary>
        /// <returns>True if it was added, false if it was already there</returns>
        public static bool AddIfDistinct<T>([NotNull] this ICollection<T> collection, T obj, [NotNull] IEqualityComparer<T> comparer)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            bool isDistinct = !collection.Contains(obj, comparer);
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            int i = 0;
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            int i = 0;
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            var asList = enumerable as IList<T> ?? enumerable.ToArray();
            for (int i = asList.Count - 1; i >= 0; i--)
            {
                if (comparer.Equals(asList[i], element))
                    return i;
            }
            return -1;
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var asList = enumerable as IList<T> ?? enumerable.ToArray();
            for (int i = asList.Count - 1; i >= 0; i--)
            {
                if (predicate(asList[i]))
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
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Count() - 1;
        }

        /// <summary>
        /// Returns the index of the last element in an array for the given dimension
        /// <returns>Index if there are any items and the dimension exists, otherwise -1</returns>
        /// </summary>
        [Pure]
        public static int LastIndex([NotNull] this Array array, int dimension = 0)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (dimension < 0)
                throw new ArgumentOutOfRangeException(nameof(dimension), "Cannot be negative");
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
            if (dic == null)
                throw new ArgumentNullException(nameof(dic));

            TValue result;
            return dic.TryGetValue(key, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Sets allocated items in a list to the given value
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value, int startIndex, int count)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Cannot be negative");
            if (startIndex >= list.Count)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Out of bounds");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (startIndex + count > list.Count)
                throw new ArgumentOutOfRangeException(nameof(count), "Out of bounds");
            if (count == 0)
                return;

            for (int i = startIndex; i < startIndex + count; i++)
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
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");

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
            if (dic == null)
                throw new ArgumentNullException(nameof(dic));

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