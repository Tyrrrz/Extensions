using System;
using System.Linq;
using System.Xml.Linq;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Strips namespaces from this element, its attributes and repeats the same for all its children
        /// </summary>
        [Pure, NotNull]
        public static XElement StripNamespaces([NotNull] this XElement element)
        {
            // Original code credit: http://stackoverflow.com/a/1147012

            if (element == null)
                throw new ArgumentNullException(nameof(element));

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
    }
}