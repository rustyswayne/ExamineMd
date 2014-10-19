namespace ExamineMd.Models
{
    /// <summary>
    /// Defines an MdEntity.
    /// </summary>
    public interface IMdEntity
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the file with respect to the file store root.
        /// </summary>
        IMdPath Path { get; set; } 
    }
}