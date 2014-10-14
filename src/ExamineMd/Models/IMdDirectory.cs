namespace ExamineMd.Models
{
    using System.IO;

    /// <summary>
    /// Defines a MdDirectory.
    /// </summary>
    internal interface IMdDirectory
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        IMdPath Path { get; set; }

        /// <summary>
        /// Gets or sets the directory info.
        /// </summary>
        DirectoryInfo DirectoryInfo { get; set; }
    }
}