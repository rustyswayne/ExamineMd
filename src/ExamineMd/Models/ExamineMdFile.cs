namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Represents an ExamineMdFile
    /// </summary>
    public class ExamineMdFile : IExamineMdFile
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
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}