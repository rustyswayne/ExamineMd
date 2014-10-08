namespace ExamineMd.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using ExamineMd.Models;
    using ExamineMd.Persistence.Factories;

    using Umbraco.Core.IO;

    /// <summary>
    /// Represents a MarkdownFileRepository.
    /// </summary>
    internal class MarkdownFileRepository : IMarkdownFileRepository
    {
        /// <summary>
        /// The <see cref="DirectoryInfo"/> for the local markdown file store
        /// </summary>
        private DirectoryInfo _root;

        /// <summary>
        /// The <see cref="ExamineMdFileFactory"/>.
        /// </summary>
        private Lazy<ExamineMdFileFactory> _factory; 

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFileRepository"/> class.
        /// </summary>
        public MarkdownFileRepository()
            : this(ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFileRepository"/> class.
        /// </summary>
        /// <param name="pathToRoot">
        /// The path to root.
        /// </param>
        internal MarkdownFileRepository(string pathToRoot)
        {
            Mandate.ParameterNotNullOrEmpty(pathToRoot, "pathToRoot");

            Initialize(pathToRoot);
        }

        /// <summary>
        /// Searches the Markdown file store by path and an optional fileNameFilter
        /// </summary>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="ExamineMdFile"/> that matches the fileName
        /// </returns>
        public IEnumerable<IExamineMdFile> Search(string fileName)
        {
            throw new System.NotImplementedException();
        }

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
        /// A collection of <see cref="ExamineMdFile"/> that matches the path and fileName
        /// </returns>
        public IEnumerable<IExamineMdFile> Search(string path, string fileName)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all <see cref="IExamineMdFile"/> in the file store.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IExamineMdFile}"/>.
        /// </returns>
        /// <remarks>
        /// TODO Dina - This is the method used by to Index the md files in Examine 
        /// </remarks>
        public IEnumerable<IExamineMdFile> GetAllMarkdownFiles()
        {
            return GetMarkdownFiles(_root);
        }

        /// <summary>
        /// Recursively gets a collection of Markdown files starting at the directory specified
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IExamineMdFile"/>.
        /// </returns>
        private IEnumerable<IExamineMdFile> GetMarkdownFiles(DirectoryInfo directory)
        {

            var files = new List<IExamineMdFile>();
            var subs = directory.GetDirectories();
            foreach (var sub in subs)
            {
                files.AddRange(this.GetMarkdownFiles(sub));
            }

            files.AddRange(directory.GetFiles("*.md").Select(x => _factory.Value.Build(x)));

            return files;
        }

        /// <summary>
        /// Responsible for initializing the repository.
        /// </summary>
        /// <param name="pathToRoot">
        /// The path to the root of the Markdown file store
        /// </param>
        private void Initialize(string pathToRoot)
        {

            var fullPath = IOHelper.MapPath(pathToRoot);

            _root = new DirectoryInfo(fullPath);

            _factory = new Lazy<ExamineMdFileFactory>(() => new ExamineMdFileFactory(fullPath));
        }
    }
}