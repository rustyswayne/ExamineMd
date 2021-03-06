﻿namespace ExamineMd.Models
{
    /// <summary>
    /// Represents a MdMetaItem.
    /// </summary>
    public class MdDataItem
    {
        /// <summary>
        /// Gets or sets the group.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }
    }
}