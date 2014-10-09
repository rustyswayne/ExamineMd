namespace ExamineMd.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Xml.Linq;

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
                           Path = fi.DirectoryName == null ? this.PathToRoot : fi.DirectoryName.Replace(this.PathToRoot, string.Empty),
                           FileName = fi.Name,
                           Title = this.GetTitleFromFileName(fi.Name),
                           Body = File.ReadAllText(fi.FullName),
                           MetaData = this.GetMetaItems(fi),
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
                Path = directory.Name.Replace(this.PathToRoot, string.Empty),
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


            return this._textInfo.ToTitleCase(name);
        }

        /// <summary>
        /// The get meta items.
        /// </summary>
        /// <param name="mdFile">
        /// The md file.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<IMdMetaItem> GetMetaItems(FileInfo mdFile)
        {
            var meta = new List<IMdMetaItem>();
         
            var fullName = mdFile.FullName;

            if (!fullName.EndsWith(".md")) return meta;

            var metaPath = string.Format("{0}{1}", mdFile.FullName.Substring(0, mdFile.FullName.Length - 2), "xml");

            if (!File.Exists(metaPath)) return meta;

            var xdoc = XDocument.Parse(File.ReadAllText(metaPath));

            meta.AddRange(xdoc.Descendants("item").Select(item => new MdMetaItem() { Group = item.Attribute("group").Value, Alias = item.Attribute("alias").Value, Value = item.Attribute("value").Value }));

            return meta;
        }
    }
}