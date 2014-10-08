namespace ExamineMd.Persistence.Repositories
{
    using System.Collections;
    using System.Collections.Generic;

    using ExamineMd.Models;

    /// <summary>
    /// The MarkdownFileRepository interface.
    /// </summary>
    internal interface IMarkdownFileRepository
    {
        /// <summary>
        /// Searches the Markdown file store by path and an optional fileNameFilter
        /// </summary>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="IExamineMdFile"/> that matches the fileName
        /// </returns>
        IEnumerable<IExamineMdFile> Search(string fileName); 
            
        /// <summary>
        /// Searches the Markdown file store by path and an optional fileNameFilter
        /// </summary>
        /// <param name="path">
        /// The relative path to the file. Example /directory1/directory2/
        /// </param>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="IExamineMdFile"/> that matches the path and fileName
        /// </returns>
        IEnumerable<IExamineMdFile> Search(string path, string fileName);

        /// <summary>
        /// Gets a collection of all Markdown files in the file store.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IExamineMdFile}"/>.
        /// </returns>
        IEnumerable<IExamineMdFile> GetAllMarkdownFiles();
    }
}