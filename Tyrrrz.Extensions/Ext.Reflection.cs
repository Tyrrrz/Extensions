using System;
using System.IO;
using System.Reflection;
using System.Resources;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Reads the given manifest resource as a string.
        /// </summary>
        [Pure, NotNull]
        public static string GetManifestResourceString([NotNull] this Assembly assembly, [NotNull] string resourceName)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));
            if (resourceName == null)
                throw new ArgumentNullException(nameof(resourceName));

            // Get manifest stream
            var stream = assembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                throw new MissingManifestResourceException($"Could not find resource [{resourceName}].");

            // Read stream
            using (stream)
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}