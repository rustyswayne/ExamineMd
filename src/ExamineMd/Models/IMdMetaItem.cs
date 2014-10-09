namespace ExamineMd.Models
{
    /// <summary>
    /// Defines a MdMetaItem.
    /// </summary>
    public interface IMdMetaItem
    {
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        string Group { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        string Value { get; set; }
    }
}