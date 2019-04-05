using System;
using System.IO;
using System.Reflection;
using System.Resources;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see href="Assembly" />.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Reads the given manifest resource as a string.
        /// </summary>
        [Pure, NotNull]
        public static string GetManifestResourceString([NotNull] this Assembly assembly, [NotNull] string resourceName)
        {
            assembly.GuardNotNull(nameof(assembly));
            resourceName.GuardNotNull(nameof(resourceName));

            // Get manifest stream
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new MissingManifestResourceException($"Resource [{resourceName}] doesn't exist.");

            // Read stream
            using (stream)
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}