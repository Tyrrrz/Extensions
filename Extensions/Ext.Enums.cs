using System;
using System.Collections.Generic;
using System.Linq;
using Tyrrrz.Extensions.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Returns true if the enum value has all of the given flags set
        /// </summary>
        [Pure]
        public static bool HasFlags(this Enum enumValue, params Enum[] flags)
        {
            return flags.All(enumValue.HasFlag);
        }

        /// <summary>
        /// Parses string to enum
        /// </summary>
        [Pure]
        public static TEnum ParseEnum<TEnum>([NotNull] this string str) where TEnum : struct
        {
            if (IsBlank(str))
                throw new ArgumentNullException(nameof(str));

            return (TEnum) Enum.Parse(typeof (TEnum), str, true);
        }

        /// <summary>
        /// Parses string to enum or default value
        /// </summary>
        [Pure]
        public static TEnum ParseEnumOrDefault<TEnum>(this string str, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            if (IsBlank(str)) return defaultValue;
            TEnum result;
            return Enum.TryParse(str, true, out result) ? result : defaultValue;
        }

        /// <summary>
        /// Converts an integer to an enum value that it corresponds to
        /// </summary>
        [Pure]
        public static TEnum ToEnum<TEnum>(this int enumIntegerValue) where TEnum : struct
        {
            return (TEnum) Enum.ToObject(typeof (TEnum), enumIntegerValue);
        }

        /// <summary>
        /// Converts an integer to an enum value that it corresponds to or a default value if not successful
        /// </summary>
        [Pure]
        public static TEnum ToEnumOrDefault<TEnum>(this int enumIntegerValue, TEnum defaultValue = default(TEnum)) where TEnum : struct
        {
            try
            {
                return ToEnum<TEnum>(enumIntegerValue);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// Gets all possible values of an enumeration
        /// </summary>
        [Pure]
        public static IEnumerable<TEnum> GetAllEnumValues<TEnum>() where TEnum : struct
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        /// <summary>
        /// Gets all possible values of an enumeration
        /// </summary>
        [Pure]
        public static IEnumerable<TEnum> GetAllEnumValues<TEnum>(this Type enumType) where TEnum : struct
        {
            return Enum.GetValues(enumType).Cast<TEnum>();
        }

        /// <summary>
        /// Returns the path of the given special folder
        /// </summary>
        [Pure]
        public static string GetPath(this Environment.SpecialFolder specialFolder,
            Environment.SpecialFolderOption option = Environment.SpecialFolderOption.None)
        {
            return Environment.GetFolderPath(specialFolder, option);
        }
    }
}