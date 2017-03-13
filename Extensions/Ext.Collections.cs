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
        /// If the <see cref="IEnumerable{T}"/> is null returns false, otherwise works the same as <see cref="Enumerable.Any{T}(IEnumerable{T},Func{T,bool})"/>
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
        /// If the <see cref="IEnumerable{T}"/> is null returns false, otherwise works the same as <see cref="Enumerable.Any{T}(IEnumerable{T})"/>
        /// </summary>
        [Pure]
        [ContractAnnotation("enumerable:null => false")]
        public static bool NotNullAndAny<T>([CanBeNull] this IEnumerable<T> enumerable)
        {
            return enumerable != null && enumerable.Any();
        }

        /// <summary>
        /// Returns a random member of an <see cref="IEnumerable{T}"/>
        /// </summary>
        [Pure]
        public static T GetRandom<T>([NotNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            var list = enumerable as IList<T> ?? enumerable.ToArray();
            if (list.Count <= 1) return list.First();
            return list[SharedInstances.Random.Next(0, list.Count)];
        }

        /// <summary>
        /// Returns a random member of an <see cref="IEnumerable{T}"/> or default if it's empty
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
        /// If the given <see cref="IEnumerable{T}"/> is null, returns an empty <see cref="IEnumerable{T}"/>.
        /// Otherwise returns given <see cref="IEnumerable{T}"/>.
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
        public static IEnumerable<TSource> Distinct<TSource, TKey>([NotNull] this IEnumerable<TSource> enumerable, [NotNull] Func<TSource, TKey> keySelector)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));

            var existing = new HashSet<TKey>();
            foreach (var element in enumerable)
            {
                if (existing.Add(keySelector(element)))
                    yield return element;
            }
        }

        /// <summary>
        /// Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one sequence.
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> SelectMany<T>([NotNull] this IEnumerable<IEnumerable<T>> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.SelectMany(i => i);
        }

        /// <summary>
        /// Filters the given <see cref="IEnumerable{T}"/> returning a new one, consisting only of items NOT equal to <paramref name="value"/>
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> Except<T>([NotNull] this IEnumerable<T> enumerable, T value)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Where(i => !Equals(i, value));
        }

        /// <summary>
        /// Filters the given <see cref="IEnumerable{T}"/> returning a new one, consisting only of items NOT equal to <paramref name="value"/>
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
        /// Filters the given <see cref="IEnumerable{T}"/> returning a new one, consisting only of items that don't have default value
        /// </summary>
        [Pure, NotNull]
        public static IEnumerable<T> ExceptDefault<T>([NotNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return Except(enumerable, default(T));
        }

        /// <summary>
        /// Trims the <see cref="IEnumerable{T}"/> returning a new one, consisting only of the last <paramref name="count"/> items
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

            return enumerable.Reverse().Take(count).Reverse();
        }

        /// <summary>
        /// Trims the <see cref="IEnumerable{T}"/> returning a new one, disposing of the last <paramref name="count"/> items
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

            return enumerable.Reverse().Skip(count).Reverse();
        }

        /// <summary>
        /// Invokes a delegate on every member of an <see cref="IEnumerable{T}"/>
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
        /// Creates a <see cref="HashSet{T}"/> from a <see cref="IEnumerable{T}"/>
        /// </summary>
        [Pure, NotNull]
        public static HashSet<T> ToHashSet<T>([NotNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return new HashSet<T>(enumerable);
        }

        /// <summary>
        /// Creates a <see cref="HashSet{T}"/> from a <see cref="IEnumerable{T}"/>
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
        /// Adds a new item to an <see cref="IList{T}"/> if it's not there yet
        /// </summary>
        /// <returns>True if it was added, false if it was already there</returns>
        public static bool AddIfDistinct<T>([NotNull] this ICollection<T> collection, T obj)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            bool isDistinct = !collection.Contains(obj);
            if (isDistinct) collection.Add(obj);
            return isDistinct;
        }

        /// <summary>
        /// Adds a new item to an <see cref="IList{T}"/> if it's not there yet
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
        /// Searches for an item in an <see cref="IList{T}"/> returning its index
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IList<T> list, T element)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (int i = 0; i < list.Count; i++)
            {
                if (Equals(list[i], element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item in an <see cref="IList{T}"/> returning its index
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IList<T> list, T element, [NotNull] IEqualityComparer<T> comparer)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));

            for (int i = 0; i < list.Count; i++)
            {
                if (comparer.Equals(list[i], element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item in an <see cref="IList{T}"/> returning its index
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int IndexOf<T>([NotNull] this IList<T> list, [NotNull] Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item in an <see cref="IList{T}"/> returning index of its last occurence
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, T element)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (Equals(list[i], element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item in an <see cref="IList{T}"/> returning index of its last occurence
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, T element, [NotNull] IEqualityComparer<T> comparer)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (comparer.Equals(list[i], element))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Searches for an item in an <see cref="IList{T}"/> returning index of its last occurence
        /// <returns>Item index if found, -1 if not found</returns>
        /// </summary>
        [Pure]
        public static int LastIndexOf<T>([NotNull] this IList<T> list, [NotNull] Func<T, bool> predicate)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the last index of an <see cref="IEnumerable{T}"/>
        /// <returns>Last index if there are any items, -1 if there aren't</returns>
        /// </summary>
        [Pure]
        public static int LastIndex<T>([NotNull] this IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            return enumerable.Count() - 1;
        }

        /// <summary>
        /// Returns the last index of an <see cref="IList{T}"/>
        /// <returns>Last index if there are any items, -1 if there aren't</returns>
        /// </summary>
        [Pure]
        public static int LastIndex<T>([NotNull] this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return list.Count - 1;
        }

        /// <summary>
        /// Returns the last index of an array for the given dimension
        /// <returns>Last index if there are any items, -1 if there aren't</returns>
        /// </summary>
        [Pure]
        public static int LastIndex<T>([NotNull] this T[] array, int dimension = 0)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (dimension < 0)
                throw new ArgumentOutOfRangeException(nameof(dimension), "Cannot be negative");

            return array.GetUpperBound(dimension);
        }

        /// <summary>
        /// Returns the value of an <see cref="IList{T}"/> by given <paramref name="index"/> or default if out of bounds
        /// </summary>
        [Pure]
        public static T GetOrDefault<T>([NotNull] this IList<T> list, int index,
            T defaultValue = default(T))
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            return index < 0 || index > list.Count - 1 ? defaultValue : list[index];
        }

        /// <summary>
        /// Returns the value of a <see cref="IDictionary{TKey,TValue}"/>, based on key or default if it doesn't exist
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
        /// Sets all values in an <see cref="IList{T}"/> to given <paramref name="value"/>
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            for (int i = 0; i < list.Count; i++)
                list[i] = value;
        }

        /// <summary>
        /// Sets all values in an <see cref="IList{T}"/> to given <paramref name="value"/>
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value, int startIndex)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Cannot be negative");

            for (int i = startIndex; i < list.Count; i++)
                list[i] = value;
        }

        /// <summary>
        /// Sets all values in an <see cref="IList{T}"/> to given <paramref name="value"/>
        /// </summary>
        public static void Fill<T>([NotNull] this IList<T> list, T value, int startIndex, int count)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));
            if (startIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(startIndex), "Cannot be negative");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "Cannot be negative");
            if (count == 0)
                return;

            for (int i = startIndex; i < startIndex + count; i++)
                list[i] = value;
        }

        /// <summary>
        /// Makes sure that the <see cref="IList{T}"/> has no more items than specified by removing other items
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
        /// Sets the <see cref="IDictionary{TKey,TValue}"/> value for the given key or adds a new pair if it doesn't exist
        /// <returns>True if the key already existed, false if new one was added</returns>
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