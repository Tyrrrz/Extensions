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
        public override string Message { get; }

        /// <inheritdoc />
        public XmlElementNotFoundException(XName name)
        {
            Name = name;
            Message = $"XML element [{name}] was not found";
        }

        /// <inheritdoc />
        public XmlElementNotFoundException(XName name, string message)
        {
            Name = name;
            Message = message;
        }
    }
}