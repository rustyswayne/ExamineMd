namespace ExamineMd.Persistence.Factories
{
    using System;
    using System.Configuration;
    using System.IO;

    using ExamineMd.Models;

    using Umbraco.Core.IO;

    /// <summary>
    /// The examine md file factory.
    /// </summary>
    internal class ExamineMdFileFactory
    {
        private readonly string _pathToRoot;

        public ExamineMdFileFactory(string pathToRoot)
        {
            Mandate.ParameterNotNullOrEmpty(pathToRoot, "pathToRoot");

            _pathToRoot = pathToRoot;
        }

        /// <summary>
        /// Builds a <see cref="IExamineMdFile"/> from file info.
        /// </summary>
        /// <param name="fi">
        /// The <see cref="FileInfo"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IExamineMdFile"/>.
        /// </returns>
        public IExamineMdFile Build(FileInfo fi)
        {
            return new ExamineMdFile()
                       {
                           Path = fi.FullName.Replace(_pathToRoot, string.Empty),
                           FileName = fi.Name,
                           Title = this.GetTitleFromFileName(fi.Name),
                           Body = File.ReadAllText(fi.FullName),
                           DateCreated = fi.CreationTime
                       };
        }

        private string GetTitleFromFileName(string fileName)
        {
            var name = fileName.EndsWith(".md", StringComparison.InvariantCultureIgnoreCase)
                           ? fileName.Remove(fileName.Length - 3, 3)
                           : fileName;

            return name;
        }

    }
}