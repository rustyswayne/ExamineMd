namespace ExamineMd.Models
{
    using System.IO;

    /// <summary>
    /// Represents a MdDirectory.
    /// </summary>
    internal class MdDirectory : IMdDirectory
    {
        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="DirectoryInfo"/>.
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}