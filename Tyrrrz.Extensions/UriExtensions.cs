using System;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see href="Uri" />.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Converts a string to <see cref="Uri"/>.
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri)
        {
            uri.GuardNotNull(nameof(uri));
            return new UriBuilder(uri).Uri;
        }

        /// <summary>
        /// Converts a string to relative <see cref="Uri"/>.
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] Uri baseUri)
        {
            uri.GuardNotNull(nameof(uri));
            baseUri.GuardNotNull(nameof(baseUri));

            return new Uri(baseUri, new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        /// Converts a string to a relative <see cref="Uri"/> with the other string representing base URI.
        /// </summary>
        [Pure, NotNull]
        public static Uri ToUri([NotNull] this string uri, [NotNull] string baseUri)
        {
            uri.GuardNotNull(nameof(uri));
            baseUri.GuardNotNull(nameof(baseUri));

            return uri.ToUri(baseUri.ToUri());
        }

        /// <summary>
        /// Rewrites URI by setting a query parameter to given value.
        /// </summary>
        [Pure, NotNull]
        public static Uri SetQueryParameter([NotNull] this Uri uri,
            [NotNull] string key, [CanBeNull] string value)
        {
            uri.GuardNotNull(nameof(uri));
            key.GuardNotNull(nameof(key));

            // Convert URI to string
            var uriString = uri.ToString();

            // Find existing parameter
            var existingMatch = Regex.Match(uriString, $@"[?&]({Regex.Escape(key)}=?.*?)(?:&|/|$)");

            // If parameter is already set - replace with new value
            if (existingMatch.Success)
            {
                // Get the first group
                var group = existingMatch.Groups[1];

                // Remove existing
                uriString = uriString.Remove(group.Index, group.Length);

                // Insert new one
                uriString = uriString.Insert(group.Index, $"{key}={value}");
            }
            // If parameter is not set yet - append it to the end
            else
            {
                // See if there are other query parameters
                var hasOtherParams = uriString.IndexOf('?') >= 0;

                // If there are - append '&'
                if (hasOtherParams)
                    uriString += '&';
                // Otherwise - append '?'
                else
                    uriString += '?';

                // Append parameter
                uriString += $"{key}={value}";
            }

            return new Uri(uriString);
        }

        /// <summary>
        /// Rewrites URI by setting a route parameter to given value.
        /// </summary>
        [Pure, NotNull]
        public static Uri SetRouteParameter([NotNull] this Uri uri,
            [NotNull] string key, [CanBeNull] string value)
        {
            uri.GuardNotNull(nameof(uri));
            key.GuardNotNull(nameof(key));

            // Convert URI to string
            var uriString = uri.ToString();

            // Find existing parameter
            var existingMatch = Regex.Match(uriString, $@"/({Regex.Escape(key)}/?.*?)(?:/|$)");

            // If parameter is already set - replace with new value
            if (existingMatch.Success)
            {
                // Get the first group
                var group = existingMatch.Groups[1];

                // Remove existing
                uriString = uriString.Remove(group.Index, group.Length);

                // Insert new one
                uriString = uriString.Insert(group.Index, $"{key}/{value}");
            }
            // If parameter is not set yet - append it to the end
            else
            {
                // If the URI doesn't end with slash - append it
                if (uriString.ToCharArray().LastOrDefault() != '/')
                    uriString += '/';

                // Assemble new query string
                uriString += $"{key}/{value}";
            }

            return new Uri(uriString);
        }
    }
}