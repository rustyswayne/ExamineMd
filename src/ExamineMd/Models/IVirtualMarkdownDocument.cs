namespace ExamineMd.Models
{
    /// <summary>
    /// Defines a VirtualMarkdownDocument.
    /// </summary>
    public interface IVirtualMarkdownDocument : IBaseViewModel
    {
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