namespace ExamineMd.Services
{
    using System.Collections.Generic;

    using ExamineMd.Models;

    /// <summary>
    /// The MarkdownFileService interface.
    /// </summary>
    internal interface IMarkdownFileService
    {
        /// <summary>
        /// Finds the Markdown file store by it's file name
        /// </summary>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="IMdFile"/> that matches the fileName
        /// </returns>
        IEnumerable<IMdFile> Find(string fileName); 
            
        /// <summary>
        /// Searches the Markdown file store by path and an optional fileNameFilter
        /// </summary>
        /// <param name="path">
        /// The relative path to the file. Example \directory1\directory2\
        /// </param>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="IMdFile"/> that matches the path and fileName
        /// </returns>
        IEnumerable<IMdFile> Find(string path, string fileName);

        /// <summary>
        /// Lists all Markdown files found at the path indicated
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="includeChildPaths">
        /// Optional parameter to include all files in sub directories of path provided.  Defaults to false
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> List(string path, bool includeChildPaths = false); 
            
        /// <summary>
        /// Gets the Markdown file store directory represented by the path string.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IMdDirectory"/>.
        /// </returns>
        IMdDirectory GetDirectory(string path);

        /// <summary>
        /// Gets all the directories in the file store
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        IEnumerable<IMdDirectory> GetDirectories();

        /// <summary>
        /// Gets a collection of directories in the Markdown file store that match the starting path.
        /// </summary>
        /// <param name="startPath">
        /// The start path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        IEnumerable<IMdDirectory> GetDirectories(string startPath); 
            
        /// <summary>
        /// Gets a collection of all Markdown files in the file store.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        IEnumerable<IMdFile> GetAllMarkdownFiles();
    }
}