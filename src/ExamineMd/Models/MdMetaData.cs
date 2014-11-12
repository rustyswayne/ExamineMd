namespace ExamineMd.Models
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an MdFileMeta.
    /// </summary>
    internal class MdMetaData : IMdMetaData
    {
        /// <summary>
        /// Gets or sets the link title used in navigation.
        /// </summary>
        public string PageTitleLinks { get; set; }

        /// <summary>
        /// Gets or sets the page tile.
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the relevance.
        /// </summary>
        public string Relevance { get; set; }

        /// <summary>
        /// Gets or sets the revision.
        /// </summary>
        public string Revision { get; set; }

        /// <summary>
        /// Gets or sets the a custom collection of <see cref="MdDataItem"/>.
        /// </summary>
        public IEnumerable<MdDataItem> Items { get; set; }
    }
}