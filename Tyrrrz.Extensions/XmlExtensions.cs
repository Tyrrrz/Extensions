using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;
using Tyrrrz.Extensions.Internal;

namespace Tyrrrz.Extensions
{
    /// <summary>
    /// Extensions for LINQ to XML.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Returns a new element with namespaces recursively stripped from tags and attributes.
        /// </summary>
        [Pure, NotNull]
        public static XElement StripNamespaces([NotNull] this XElement element)
        {
            // Based on http://stackoverflow.com/a/1147012

            element.GuardNotNull(nameof(element));

            var result = new XElement(element);
            foreach (var e in result.DescendantsAndSelf())
            {
                // Strip namespace from name
                e.Name = XNamespace.None.GetName(e.Name.LocalName);

                // Strip namespaces from attributes
                var attributes = e.Attributes()
                    .Where(a => !a.IsNamespaceDeclaration)
                    .Where(a => a.Name.Namespace != XNamespace.Xml && a.Name.Namespace != XNamespace.Xmlns)
                    .Select(a => new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value));
                e.ReplaceAttributes(attributes);
            }

            return result;
        }
    }
}