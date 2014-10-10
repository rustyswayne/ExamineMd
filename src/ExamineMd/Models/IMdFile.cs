namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Defines an MdFile.
    /// </summary>
    public interface IMdFile
    {
        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the file with respect to the file store root.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        string FileName { get; set; }

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
        IMdFileMetaData MetaData { get; set; }
            
        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        DateTime DateCreated { get; set; }
    }
}