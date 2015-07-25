namespace ExamineMd.Models
{
    /// <summary>
    /// Defines the VirtualMarkdownListing
    /// </summary>
    public interface IVirtualMarkdownListing : IVirtualMarkdownBase
    {
        /// <summary>
        /// Gets or sets the brief summary of the contents.  Applies to listings only.
        /// </summary>
        string Brief { get; set; }

        /// <summary>
        /// Gets or sets the max list count.
        /// </summary>
        int MaxListCount { get; set; }
    }
}