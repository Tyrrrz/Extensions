using System;
using System.Xml.Linq;

namespace Tyrrrz.Extensions.Exceptions
{
    /// <summary>
    /// Thrown when an XML element was not found
    /// </summary>
    public class XmlElementNotFoundException : Exception
    {
        /// <summary>
        /// Name of an XML element which was not found
        /// </summary>
        public XName Name { get; }

        /// <inheritdoc />
        public XmlElementNotFoundException(XName name)
            : base($"XML element [{name}] was not found")
        {
            Name = name;
        }
    }
}