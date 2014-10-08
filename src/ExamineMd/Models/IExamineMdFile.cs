namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Defines an ExamineMd Markdown File.
    /// </summary>
    public interface IExamineMdFile
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
        /// Gets or sets the date created.
        /// </summary>
        DateTime DateCreated { get; set; }
    }
}