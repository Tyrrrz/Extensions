using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

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
        [return: NotNull]
        public static async Task<IEnumerable<TResult>> ParallelSelectAsync<T, TResult>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, Task<TResult>> taskFunc)
        {
            return await Task.WhenAll(source.Select(taskFunc)).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes a task asynchronously on all elements of a sequence in parallel.
        /// </summary>
        [return: NotNull]
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> source,
            [NotNull] Func<T, Task> taskFunc)
        {
            await Task.WhenAll(source.Select(taskFunc)).ConfigureAwait(false);
        }
    }
}