using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Strips namespaces from elements and their attributes recursively, starting from the given element
        /// </summary>
        [Pure, NotNull]
        public static XElement StripNamespaces([NotNull] this XElement element)
        {
            // Original code credit: http://stackoverflow.com/a/1147012

            GuardNull(element, nameof(element));

            var result = new XElement(element);
            foreach (var e in result.DescendantsAndSelf())
            {
                e.Name = XNamespace.None.GetName(e.Name.LocalName);
                var attributes = e.Attributes()
                    .Where(a => !a.IsNamespaceDeclaration)
                    .Where(a => a.Name.Namespace != XNamespace.Xml && a.Name.Namespace != XNamespace.Xmlns)
                    .Select(a => new XAttribute(XNamespace.None.GetName(a.Name.LocalName), a.Value));
                e.ReplaceAttributes(attributes);
            }

            return result;
        }

        /// <summary>
        /// Gets the first descendant with the specified name or null if none found
        /// </summary>
        [Pure, CanBeNull]
        public static XElement Descendant([NotNull] this XElement element, [NotNull] XName name)
        {
            GuardNull(element, nameof(element));
            GuardNull(name, nameof(name));

            return element.Descendants(name).FirstOrDefault();
        }

        /// <summary>
        /// Gets the first descendant with the specified name or throws an exception if not found
        /// </summary>
        [Pure, NotNull]
        public static XElement DescendantStrict([NotNull] this XElement element, [NotNull] XName name)
        {
            GuardNull(element, nameof(element));
            GuardNull(name, nameof(name));

            return Descendant(element, name) ?? throw new KeyNotFoundException($"Descendant [{name}] not found");
        }

        /// <summary>
        /// Gets the first (in document order) child element with the specified name or throws an exception if not found
        /// </summary>
        [Pure, NotNull]
        public static XElement ElementStrict([NotNull] this XElement element, [NotNull] XName name)
        {
            GuardNull(element, nameof(element));
            GuardNull(name, nameof(name));

            return element.Element(name) ?? throw new KeyNotFoundException($"Element [{name}] not found");
        }

        /// <summary>
        /// Returns an attribute with the given name or throws an exception if not found
        /// </summary>
        [Pure, NotNull]
        public static XAttribute AttributeStrict([NotNull] this XElement element, [NotNull] XName name)
        {
            GuardNull(element, nameof(element));
            GuardNull(name, nameof(name));

            return element.Attribute(name) ?? throw new KeyNotFoundException($"Attribute [{name}] not found");
        }
    }
}