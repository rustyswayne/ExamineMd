namespace ExamineMd.Persistence.Factories
{
    using System;
    using System.Globalization;
    using System.IO;

    using ExamineMd.Models;

    /// <summary>
    /// The examine md file factory.
    /// </summary>
    internal class FileStoreFactory : FileStoreFactoryBase
    {
        /// <summary>
        /// The <see cref="TextInfo"/>.
        /// </summary>
        private readonly TextInfo _textInfo = new CultureInfo("en", false).TextInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoreFactory"/> class.
        /// </summary>
        /// <param name="pathToRoot">
        /// The path to root.
        /// </param>
        public FileStoreFactory(string pathToRoot)
           : base(pathToRoot)
        {
        }

        /// <summary>
        /// Builds a <see cref="IMdFile"/> from file info.
        /// </summary>
        /// <param name="fi">
        /// The <see cref="FileInfo"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        public IMdFile Build(FileInfo fi)
        {
            return new MdFile()
                       {
                           Path = fi.DirectoryName == null ? PathToRoot : fi.DirectoryName.Replace(PathToRoot, string.Empty),
                           FileName = fi.Name,
                           Title = this.GetTitleFromFileName(fi.Name),
                           Body = File.ReadAllText(fi.FullName),
                           DateCreated = fi.CreationTime
                       };
        }

        /// <summary>
        /// Builds a <see cref="IMdDirectory"/> from directory info.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <returns>
        /// The <see cref="IMdDirectory"/>.
        /// </returns>
        internal IMdDirectory Build(DirectoryInfo directory)
        {
            return new MdDirectory()
            {
                Path = directory.Name.Replace(PathToRoot, string.Empty),
                DirectoryInfo = directory
            };
        }

        /// <summary>
        /// The get title from file name.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private string GetTitleFromFileName(string fileName)
        {
            
            var name = fileName.EndsWith(".md", StringComparison.InvariantCultureIgnoreCase)
                           ? fileName.Remove(fileName.Length - 3, 3)
                           : fileName;

            name = name.Replace("-", " ").Replace("_", " ");


            return _textInfo.ToTitleCase(name);
        }

    }
}