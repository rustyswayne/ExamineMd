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
    }
}