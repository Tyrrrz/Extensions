using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using System.Threading.Tasks;

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
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, Task> task)
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
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var tasks = enumerable.Select(async i => await Task.Run(() => action(i)).ConfigureAwait(false));
            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}