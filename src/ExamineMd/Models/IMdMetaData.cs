namespace ExamineMd.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines a MdFileMeta.
    /// </summary>
    public interface IMdMetaData
    {
        /// <summary>
        /// Gets or sets the page tile.
        /// </summary>
        string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the relevance.
        /// </summary>
        string Relevance { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        string Revision { get; set; }

        /// <summary>
        /// Gets or sets the a custom collection of <see cref="MdDataItem"/>.
        /// </summary>
        IEnumerable<MdDataItem> Items { get; set; }
    }
}