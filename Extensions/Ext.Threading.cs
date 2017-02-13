using System;
using JetBrains.Annotations;
#if Net45
using System.Threading.Tasks;
#endif

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
#if Net45
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
#endif
    }
}