namespace ExamineMd.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the VirtualMarkdownListing
    /// </summary>
    public interface IVirtualMarkdownListing : IBaseViewModel
    {
        /// <summary>
        /// Gets or sets the brief summary of the contents.  Applies to listings only.
        /// </summary>
        string Brief { get; set; }

        /// <summary>
        /// Gets or sets the max list count.
        /// </summary>
        int MaxListCount { get; set; }

        /// <summary>
        /// Gets or sets the starting path.  
        /// </summary>
        string StartingPath { get; set; }

        /// <summary>
        /// Gets the collection of <see cref="IVirtualMarkdownDocument"/>
        /// </summary>
        IEnumerable<IVirtualMarkdownDocument> Documents { get; } 
    }
}