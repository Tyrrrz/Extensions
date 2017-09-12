using System;
using System.Linq;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Parses string to the given enum
        /// </summary>
        [Pure]
        public static TEnum ParseEnum<TEnum>([NotNull] this string str, bool ignoreCase = true) where TEnum : struct
        {
            if (str == null)
                throw new ArgumentNullException(nameof(str));

            return (TEnum) Enum.Parse(typeof(TEnum), str, ignoreCase);
        }

        /// <summary>
        /// Parses string to the given enum or returns default value if unsuccessful
        /// </summary>
        [Pure]
        public static TEnum ParseEnumOrDefault<TEnum>([CanBeNull] this string str, bool ignoreCase = true,
            TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            return Enum.TryParse(str, ignoreCase, out TEnum result) ? result : defaultValue;
        }

        /// <summary>
        /// Returns a random enum value
        /// </summary>
        [Pure]
        public static TEnum RandomEnum<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>().GetRandom();
        }
    }
}