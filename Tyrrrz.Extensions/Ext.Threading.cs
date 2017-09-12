using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Continues execution without waiting for the task to complete
        /// </summary>
        public static void Forget(this Task task)
        {
            // #pragma 4014 workaround
        }

        /// <summary>
        /// Executes a task asynchronously on all elements of a sequence in parallel
        /// </summary>
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable,
            [NotNull] Func<T, Task> task)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var tasks = enumerable.Select(async i => await task(i).ConfigureAwait(false));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes an action asynchronously on all elements of a sequence in parallel
        /// </summary>
        public static Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable,
            [NotNull] Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            return ParallelForEachAsync(enumerable, i => Task.Run(() => action(i)));
        }

        /// <summary>
        /// Executes a task asynchronously on all elements of a sequence in parallel and returns a sequence of results
        /// </summary>
        [Pure]
        public static async Task<IEnumerable<TResult>> ParallelSelectAsync<T, TResult>(
            [NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, Task<TResult>> task)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var tasks = enumerable.Select(async i => await task(i).ConfigureAwait(false));
            return await Task.WhenAll(tasks).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a function asynchronously on all elements of a sequence in parallel and returns a sequence of results
        /// </summary>
        [Pure]
        public static Task<IEnumerable<TResult>> ParallelSelectAsync<T, TResult>(
            [NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, TResult> func)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (func == null)
                throw new ArgumentNullException(nameof(func));

            return ParallelSelectAsync(enumerable, i => Task.Run(() => func(i)));
        }
    }
}