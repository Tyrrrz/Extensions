using System;
using System.Net;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Converts a string to URI
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));

            return new UriBuilder(uri).Uri;
        }

        /// <summary>
        /// Converts a string to relative uri, with a given base uri
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] string baseUri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(ToUri(baseUri), new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        /// Converts a string to relative uri, with a given base uri
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] Uri baseUri)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (baseUri == null)
                throw new ArgumentNullException(nameof(baseUri));

            return new Uri(baseUri, new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        /// Returns URL encoded version of the given string
        /// </summary>
        [Pure, NotNull]
        public static string UrlEncode([NotNull] this string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

#if Net45
            return WebUtility.UrlEncode(data);
#else
            return Uri.EscapeDataString(data);
#endif
        }

        /// <summary>
        /// Returns URL decoded version of the given string
        /// </summary>
        [Pure, NotNull]
        public static string UrlDecode([NotNull] this string data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

#if Net45
            return WebUtility.UrlDecode(data);
#else
            return Uri.UnescapeDataString(data);
#endif
        }
    }
}