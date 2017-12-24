using System;
using System.Text;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Converts byte array to string.
        /// </summary>
        [Pure]
        public static string GetString(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data, 0, data.Length);
        }

        /// <summary>
        /// Converts byte array to string using unicode encoding.
        /// </summary>
        [Pure]
        public static string GetString(this byte[] data)
        {
            return data.GetString(Encoding.Unicode);
        }

        /// <summary>
        /// Converts string to byte array.
        /// </summary>
        [Pure]
        public static byte[] GetBytes(this string str, Encoding encoding)
        {
            return encoding.GetBytes(str);
        }

        /// <summary>
        /// Converts string to byte array using unicode encoding.
        /// </summary>
        [Pure]
        public static byte[] GetBytes(this string str)
        {
            return str.GetBytes(Encoding.Unicode);
        }

        /// <summary>
        /// Converts an array of bytes to a base64 string.
        /// </summary>
        [Pure]
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Converts a base64 string to a byte array.
        /// </summary>
        [Pure]
        public static byte[] FromBase64(this string str)
        {
            return Convert.FromBase64String(str);
        }
    }
}