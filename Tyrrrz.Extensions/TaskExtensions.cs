using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Task" /> and <see cref="Task{T}" />.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Executes a task asynchronously on all elements of a sequence in parallel and returns results.
        /// </summary>
        [Pure]
        public static async Task<IEnumerable<TResult>> ParallelSelectAsync<T, TResult>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, Task<TResult>> taskFunc)
        {
            source.GuardNotNull(nameof(source));
            taskFunc.GuardNotNull(nameof(taskFunc));

            return await Task.WhenAll(source.Select(taskFunc)).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a task asynchronously on all elements of a sequence in parallel.
        /// </summary>
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, Task> taskFunc)
        {
            source.GuardNotNull(nameof(source));
            taskFunc.GuardNotNull(nameof(taskFunc));

            await Task.WhenAll(source.Select(taskFunc)).ConfigureAwait(false);
        }
    }
}