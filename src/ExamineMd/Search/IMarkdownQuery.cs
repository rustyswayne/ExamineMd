namespace ExamineMd.Search
{
    using System.Collections.Generic;

    using ExamineMd.Models;

    /// <summary>
    /// The MarkdownQuery interface.
    /// </summary>
    public interface IMarkdownQuery
    {
        /// <summary>
        /// Gets a file by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        IMdFile Get(string key);

        /// <summary>
        /// Gets a <see cref="IMdFile"/> by it's path and file name.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        IMdFile Get(string path, string fileName);

        /// <summary>
        /// Searches the document store for documents matching the term specified
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> Search(string term);

        /// <summary>
        /// Searches the document store for documents matching the term specified filtered by a path.
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> Search(string term, string path);

        /// <summary>
        /// Lists all files matching a the path specified.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> List(string path);

        /// <summary>
        /// Gets a list of all Markdown documents.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> GetAll();
    }
}