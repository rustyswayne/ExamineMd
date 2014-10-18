namespace ExamineMd.Models
{
    using System.IO;

    /// <summary>
    /// Represents a MdDirectory.
    /// </summary>
    internal class MdDirectory : MdEntity, IMdDirectory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdDirectory"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public MdDirectory(IMdPath path)
            : base(path.Key)
        {
            Mandate.ParameterNotNull(path, "path");
            Path = path;
        }

        /// <summary>
        /// Gets or sets the <see cref="DirectoryInfo"/>.
        /// </summary>
        public DirectoryInfo DirectoryInfo { get; set; }
    }
}