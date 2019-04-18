using System;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see href="Enum" />.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Parses an enum value of a given type from a string.
        /// </summary>
        [Pure]
        public static TEnum ParseEnum<TEnum>([NotNull] this string value, bool ignoreCase = true) where TEnum : struct, Enum
        {
            value.GuardNotNull(nameof(value));
            return (TEnum) Enum.Parse(typeof(TEnum), value, ignoreCase);
        }

        /// <summary>
        /// Parses an enum value of a given type from a string or returns default value if unsuccessful.
        /// </summary>
        [Pure]
        public static TEnum ParseEnumOrDefault<TEnum>([CanBeNull] this string str, bool ignoreCase = true) where TEnum : struct, Enum
        {
            return Enum.TryParse(str, ignoreCase, out TEnum result) ? result : default;
        }
    }
}