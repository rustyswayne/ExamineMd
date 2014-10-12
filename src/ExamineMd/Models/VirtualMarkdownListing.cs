namespace ExamineMd.Models
{
    using System.Collections.Generic;
    using System.Linq;

    using Umbraco.Core.Models;

    /// <summary>
    /// Represents a VirtualMarkdownListing.
    /// </summary>
    public class VirtualMarkdownListing : BaseViewModel, IVirtualMarkdownListing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownListing"/> class.
        /// </summary>
        /// <param name="content">
        /// The <see cref="IPublishedContent"/>.
        /// </param>
        public VirtualMarkdownListing(IPublishedContent content)
            : this(content, Enumerable.Empty<IVirtualMarkdownDocument>())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownListing"/> class.
        /// </summary>
        /// <param name="content">
        /// The <see cref="IPublishedContent"/>.
        /// </param>
        /// <param name="documents">
        /// The collection of <see cref="IVirtualMarkdownDocument"/>
        /// </param>
        public VirtualMarkdownListing(IPublishedContent content, IEnumerable<IVirtualMarkdownDocument> documents)
            : base(content)
        {
            Mandate.ParameterNotNull(documents, "documents");

            Documents = documents;
        }

        /// <summary>
        /// Gets or sets the brief summary of the contents.  Applies to listings only.
        /// </summary>
        public string Brief { get; set; }

        /// <summary>
        /// Gets or sets the max list count.
        /// </summary>
        public int MaxListCount { get; set; }

        /// <summary>
        /// Gets or sets the starting path.
        /// </summary>
        public string StartingPath { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="IVirtualMarkdownDocument"/>
        /// </summary>
        public IEnumerable<IVirtualMarkdownDocument> Documents { get; private set; }
    }
}