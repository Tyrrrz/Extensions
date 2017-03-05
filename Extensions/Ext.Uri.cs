using System;
using System.Net;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Sets the given parameter to the given value in a query uri
        /// </summary>
        [Pure, NotNull]
        public static string SetQueryParameter([NotNull] this string uri, [NotNull] string key, [CanBeNull] string value)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            // Find existing parameter
            var existingMatch = Regex.Match(uri, $@"[?&]({key}=?.*?)(?:&|$)");

            // Parameter already set to something
            if (existingMatch.Success)
            {
                var group = existingMatch.Groups[1];

                // Remove existing
                uri = uri.Remove(group.Index, group.Length);

                // Insert new one
                uri = uri.Insert(group.Index, value != null ? $"{key}={value}" : $"{key}=");

                return uri;
            }
            // Parameter hasn't been set yet
            else
            {
                // See if there are other parameters
                bool hasOtherParams = uri.IndexOf('?') >= 0;

                // Prepend either & or ? depending on that
                string separator = hasOtherParams ? "&" : "?";

                // Assemble new query string
                return uri + separator + key + "=" + value;
            }
        }

        /// <summary>
        /// Sets the given parameter to the given value in a query uri
        /// </summary>
        [Pure, NotNull]
        public static Uri SetQueryParameter([NotNull] this Uri uri, [NotNull] string key, [CanBeNull] string value)
        {
            if (uri == null)
                throw new ArgumentNullException(nameof(uri));
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            return ToUri(SetQueryParameter(uri.ToString(), key, value));
        }
    }
}