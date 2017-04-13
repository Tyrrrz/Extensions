using System;
using System.Linq;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Returns true if the enum value has all of the given flags set
        /// </summary>
        [Pure]
        public static bool HasFlags([NotNull] this Enum enumValue, [ItemNotNull] params Enum[] flags)
        {
            GuardNull(enumValue, nameof(enumValue));

            return flags.All(enumValue.HasFlag);
        }

        /// <summary>
        /// Parses string to the given enum
        /// </summary>
        [Pure]
        public static TEnum ParseEnum<TEnum>([NotNull] this string str, bool ignoreCase = true) where TEnum : struct
        {
            GuardNull(str, nameof(str));

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