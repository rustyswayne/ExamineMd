namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Represents an ExamineMdFile
    /// </summary>
    internal class ExamineMdFile : IExamineMdFile
    {
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