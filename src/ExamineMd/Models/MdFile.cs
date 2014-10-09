namespace ExamineMd.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an MdFile
    /// </summary>
    public class MdFile : IMdFile
    {
        /// <summary>
        /// Gets or sets the relative path to the file with respect to the file store root.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the document metadata.
        /// </summary>
        public IEnumerable<IMdMetaItem> MetaData { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}