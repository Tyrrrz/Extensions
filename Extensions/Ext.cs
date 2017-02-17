using System;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extension methods for rapid development
    /// </summary>
    public partial class Ext
    {
        /// <summary>
        /// Generic string parse delegate
        /// </summary>
        public delegate T ParseDelegate<out T>(string str);

        /// <summary>
        /// Generic string try-parse delegate
        /// </summary>
        public delegate bool TryParseDelegate<T>(string str, out T result);

        private static readonly Random Random = new Random();

        /// <summary>
        /// Default comparison for methods that compare strings
        /// </summary>
        public static StringComparison DefaultStringComparison { get; set; } = StringComparison.InvariantCultureIgnoreCase;
    }
}