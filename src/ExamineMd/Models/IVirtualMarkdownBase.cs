namespace ExamineMd.Models
{
    using Umbraco.Core.Models;

    /// <summary>
    /// Defines the ExamineMd VirtualMarkdownBase
    /// </summary>
    public interface IVirtualMarkdownBase : IPublishedContent
    {
        /// <summary>
        /// Gets the content from the ExamineMd root.
        /// </summary>
        IPublishedContent RootContent { get; }

          /// <summary>
        /// Gets the starting path.
        /// </summary>
        IMdPath StartingPath { get; }

        /// <summary>
        /// Gets or sets the HTML Page Title.
        /// </summary>
        string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the page's headline.
        /// </summary>
        string HeadleLine { get; set; }

        /// <summary>
        /// Gets a value indicating whether is a document.
        /// </summary>
        bool IsDocument { get; }

        /// <summary>
        /// Gets or sets the examine md path.
        /// </summary>
        IMdPath MdPath { get; set; }
    }
}