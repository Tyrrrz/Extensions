using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Resources;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for <see cref="Assembly" />.
    /// </summary>
    public static class AssemblyExtensions
    {
        /// <summary>
        /// Reads the given manifest resource as a string.
        /// </summary>
        [return: NotNull]
        public static string GetManifestResourceString([NotNull] this Assembly assembly, [NotNull] string resourceName)
        {
            // Get manifest resource stream
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