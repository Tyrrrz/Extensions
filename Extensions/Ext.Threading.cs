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
        /// Runs the task synchronously and returns the result
        /// </summary>
        public static T GetResult<T>([NotNull] this Task<T> task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            return task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Runs the task synchronously
        /// </summary>
        public static void GetResult([NotNull] this Task task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            task.GetAwaiter().GetResult();
        }

        /// <summary>
        /// Executes a task on all elements of enumerable in parallel asynchronously
        /// </summary>
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Func<T, Task> task)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            var tasks = enumerable.Select(async i => await task(i));
            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Executes an action on all elements of enumerable in parallel asynchronously
        /// </summary>
        public static async Task ParallelForEachAsync<T>([NotNull] this IEnumerable<T> enumerable, [NotNull] Action<T> action)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            var tasks = enumerable.Select(async i => await Task.Run(() => action(i)));
            await Task.WhenAll(tasks);
        }
    }
}