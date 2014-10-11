namespace ExamineMd.Models
{
    using Umbraco.Core.Models;

    /// <summary>
    /// Defines a MarkdownVirtualContent.
    /// </summary>
    public interface IMarkdownVirtualContent : IPublishedContent
    {
        /// <summary>
        /// Gets the page title.
        /// </summary>
        string PageTitle { get; }

        /// <summary>
        /// Gets the meta description.
        /// </summary>
        string MetaDescription { get; }

        /// <summary>
        /// Gets the relevance.
        /// </summary>
        string Relevance { get; }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        string Revision { get; }

        /// <summary>
        /// Gets the <see cref="IMdFile"/>.
        /// </summary>
        IMdFile Markdown { get; }
    }
}