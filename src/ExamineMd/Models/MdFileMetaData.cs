using System.Collections;
using System.Collections.Generic;

namespace ExamineMd.Models
{
    /// <summary>
    /// Represents an MdFileMeta.
    /// </summary>
    internal class MdFileMetaData : IMdFileMetaData
    {
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
        /// Gets or sets the a custom collection of <see cref="MdMetaDataItem"/>.
        /// </summary>
        public IEnumerable<MdMetaDataItem> Items { get; set; }
    }
}