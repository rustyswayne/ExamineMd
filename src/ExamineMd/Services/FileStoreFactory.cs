using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ExamineMd.Services
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using ExamineMd.Models;
    using ExamineMd.Search;

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
           
            var path = fi.DirectoryName == null ? string.Empty : fi.DirectoryName.Replace(PathToRoot.Substring(0, PathToRoot.Length - 1), string.Empty);
            path = path.StartsWith("\\") ? path.Remove(0, 1) : path;

            var md = new MdFile()
                       {
                           Path = path,
                           FileName = fi.Name,
                           Title = this.GetTitleFromFileName(fi.Name),
                           Body = File.ReadAllText(fi.FullName),
                           MetaData = this.BuildMetaData(fi),
                           DateCreated = fi.CreationTime
                       };

            md.Key = SearchHelper.GetFileKey(md.Path, md.FileName);

            return md;
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
        /// <param name="md">
        /// The md file.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IMdFileMetaData BuildMetaData(FileInfo md)
        {
            var meta = new MdFileMetaData() { Items = Enumerable.Empty<MdMetaDataItem>() };
         
            var fullName = md.FullName;

            if (!fullName.EndsWith(".md")) return meta;

            var metaPath = string.Format("{0}{1}", md.FullName.Substring(0, md.FullName.Length - 2), "xml");

            if (!File.Exists(metaPath)) return meta;

            var xdoc = XDocument.Parse(File.ReadAllText(metaPath));

            var root = xdoc.Root;

            if (root == null) return meta;

            meta.PageTile = root.GetSafeAttribute("pageTitle");
            meta.MetaDescription = root.GetSafeAttribute("metaDescription");
            meta.Relevance = root.GetSafeAttribute("relevance");
            meta.Revision = root.GetSafeAttribute("revision");

            meta.Items = root.Descendants("item")
                .Select(item => new MdMetaDataItem()
                {
                    Group = item.Attribute("group").Value, 
                    Alias = item.Attribute("alias").Value, 
                    Value = item.Attribute("value").Value
                }).ToList();

            return meta;
        }       
    }

    /// <summary>
    /// Utiltiy extensions for XElement
    /// </summary>
    internal static class XElementExtensions
    {
        /// <summary>
        /// Gets an attribute value
        /// </summary>
        /// <param name="el">
        /// The el.
        /// </param>
        /// <param name="attName">
        /// The att name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GetSafeAttribute(this XElement el, string attName)
        {
            if (!el.HasAttributes) return string.Empty;

            return el.Attribute(attName) == null ? string.Empty : el.Attribute(attName).Value;
        }
    }
}