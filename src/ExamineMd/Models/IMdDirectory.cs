namespace ExamineMd.Models
{
    using System.IO;

    /// <summary>
    /// Defines a MdDirectory.
    /// </summary>
    internal interface IMdDirectory : IMdEntity
    {

        /// <summary>
        /// Gets or sets the directory info.
        /// </summary>
        DirectoryInfo DirectoryInfo { get; set; }
    }
}