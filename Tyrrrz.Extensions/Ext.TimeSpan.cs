using System;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Multiplies the given timespan by a scalar value.
        /// </summary>
        [Pure]
        public static TimeSpan Multiply(this TimeSpan timeSpan, double multiplier)
        {
            return TimeSpan.FromMilliseconds(timeSpan.TotalMilliseconds * multiplier);
        }

        /// <summary>
        /// Divides the given timespan by a scalar value.
        /// </summary>
        [Pure]
        public static TimeSpan Divide(this TimeSpan timeSpan, double divider)
        {
            return TimeSpan.FromMilliseconds(timeSpan.TotalMilliseconds / divider);
        }
    }
}