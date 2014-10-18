namespace ExamineMd.Models
{
    /// <summary>
    /// Defines a MdPath.
    /// </summary>
    public interface IMdPath
    {
        /// <summary>
        /// Gets the path key.
        /// </summary>
        string Key { get;  }

        /// <summary>
        /// Gets the path value.
        /// </summary>
        string Value { get;  }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets a value indicating whether is a document
        /// </summary>
        bool IsDocument { get; }
    }
}