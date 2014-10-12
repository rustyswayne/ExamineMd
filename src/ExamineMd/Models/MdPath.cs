namespace ExamineMd.Models
{
    using Umbraco.Core;

    /// <summary>
    /// Represents a MdPath.
    /// </summary>
    public class MdPath : IMdPath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdPath"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public MdPath(string path)
        {
            this.Value = path.IsNullOrWhiteSpace() ? "\\" : path;
        }

        /// <summary>
        /// Gets the path value.
        /// </summary>
        public string Value { get; private set; }
    }
}