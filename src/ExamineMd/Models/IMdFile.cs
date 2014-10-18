namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Defines an MdFile.
    /// </summary>
    public interface IMdFile : IMdEntity
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        string Body { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        IMdMetaData MetaData { get; set; }
            
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        DateTime DateCreated { get; set; }
    }
}