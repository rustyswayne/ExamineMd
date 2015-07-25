namespace ExamineMd.Services
{
    using System.Xml.Linq;

    /// <summary>
    /// Utility extensions for XElement
    /// </summary>
    internal static class XElementExtensions
    {
        /// <summary>
        /// Gets an attribute value
        /// </summary>
        /// <param name="el">
        /// The <see cref="XElement"/>
        /// </param>
        /// <param name="attName">
        /// The attribute name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GetSafeAttribute(this XElement el, string attName)
        {
            if (!el.HasAttributes) return string.Empty;

            return el.Attribute(attName) == null ? string.Empty : el.Attribute(attName).Value;
        }
    }
}