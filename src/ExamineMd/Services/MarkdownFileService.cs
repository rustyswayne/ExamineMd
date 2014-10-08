﻿namespace ExamineMd.Persistence.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;

    using ExamineMd.Models;
    using ExamineMd.Persistence.Factories;
    using ExamineMd.Services;

    using Umbraco.Core.IO;

    /// <summary>
    /// Represents a MarkdownFileService.
    /// </summary>
    internal class MarkdownFileService : IMarkdownFileService
    {
        /// <summary>
        /// The <see cref="DirectoryInfo"/> for the local markdown file store
        /// </summary>
        private DirectoryInfo _root;

        /// <summary>
        /// The <see cref="FileStoreFactory"/>.
        /// </summary>
        private Lazy<FileStoreFactory> _factory; 


        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFileService"/> class.
        /// </summary>
        public MarkdownFileService()
            : this(ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownFileService"/> class.
        /// </summary>
        /// <param name="pathToRoot">
        /// The path to root.
        /// </param>
        internal MarkdownFileService(string pathToRoot)
        {
            Mandate.ParameterNotNullOrEmpty(pathToRoot, "pathToRoot");

            Initialize(pathToRoot);
        }

        /// <summary>
        /// Finds the Markdown file store by it's file name
        /// </summary>
        /// <param name="fileName">
        /// An optional file name
        /// </param>
        /// <returns>
        /// A collection of <see cref="MdFile"/> that matches the fileName
        /// </returns>
        public IEnumerable<IMdFile> Find(string fileName)
        {
            return this.GetAllMarkdownFiles().Where(x => x.FileName.StartsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
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
        /// A collection of <see cref="MdFile"/> that matches the path and fileName
        /// </returns>
        public IEnumerable<IMdFile> Find(string path, string fileName)
        {
            path = path.StartsWith("~") ? path.Remove(0, 1) : path;
            path = path.StartsWith("/") ? path.Remove(0, 1) : path;

            return GetAllMarkdownFiles().Where(x => x.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase) && x.FileName.StartsWith(fileName, StringComparison.InvariantCultureIgnoreCase));
        }

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
        public IEnumerable<IMdFile> List(string path, bool includeChildPaths = false)
        {
            var allMatches = GetMarkdownFiles(GetDirectory(path).DirectoryInfo);

            return includeChildPaths
                       ? allMatches
                       : allMatches.Where(x => x.Path.Equals(path, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Gets a collection of all <see cref="IMdFile"/> in the file store.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        /// <remarks>
        /// TODO Dina - This is the method used by to Index the md files in Examine 
        /// </remarks>
        public IEnumerable<IMdFile> GetAllMarkdownFiles()
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
        /// The collection of <see cref="IMdFile"/>.
        /// </returns>
        private IEnumerable<IMdFile> GetMarkdownFiles(DirectoryInfo directory)
        {
            var files = new List<IMdFile>();

            files.AddRange(directory.GetFiles("*.md").Select(x => _factory.Value.Build(x)));

            var subs = directory.GetDirectories();
            foreach (var sub in subs)
            {
                files.AddRange(this.GetMarkdownFiles(sub));
            }

            return files;
        }

        /// <summary>
        /// Gets all the directories in the file store
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        public IMdDirectory GetDirectory(string path)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets all the directories in the file store
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        public IEnumerable<IMdDirectory> GetDirectories()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of directories in the Markdown file store that match the starting path.
        /// </summary>
        /// <param name="startPath">
        /// The start path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        public IEnumerable<IMdDirectory> GetDirectories(string startPath)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// The get markdown directories.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdDirectory}"/>.
        /// </returns>
        private IEnumerable<IMdDirectory> GetMarkdownDirectories(DirectoryInfo directory)
        {
            var directories = new List<IMdDirectory> { this._factory.Value.Build(directory) };

            foreach (var sub in directory.GetDirectories())
            {
                directories.AddRange(this.GetMarkdownDirectories(sub));
            }

            return directories;
        }

        private string GetPhysicalPath(string path)
        {
            var start = path.StartsWith(".") ? path.Remove(0, 1) : path;
            start = start.Replace("/", "\\");

            return IOHelper.MapPath(start);
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

            _factory = new Lazy<FileStoreFactory>(() => new FileStoreFactory(fullPath));
        }
    }
}