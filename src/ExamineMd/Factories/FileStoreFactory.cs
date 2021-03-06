﻿namespace ExamineMd.Factories
{
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    using ExamineMd.Models;
    using ExamineMd.Services;

    /// <summary>
    /// The examine md file factory.
    /// </summary>
    internal class FileStoreFactory : FileStoreFactoryBase
    {
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
            var metaData = BuildMetaData(fi);
            var md = new MdFile(SearchHelper.GetFileKey(fi.DirectoryName.GetFileStoreReferencePath(), fi.Name), metaData)
                       {
                           Path = new MdPath(SearchHelper.GetPathKey(fi.DirectoryName.GetFileStoreReferencePath()), fi.DirectoryName.GetFileStoreReferencePath(), fi.Name),                           
                           Title = fi.Name.AsFormattedTitle(),
                           Body = File.ReadAllText(fi.FullName),
                           DateCreated = fi.CreationTime
                       };
            
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
            return new MdDirectory(new MdPath(directory.FullName.RelativePathFromFileStoreRoot()))
            {
                DirectoryInfo = directory
            };
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
        private static IMdMetaData BuildMetaData(FileInfo md)
        {
            var meta = new MdMetaData() { Items = Enumerable.Empty<MdDataItem>() };
         
            var fullName = md.FullName;

            if (!fullName.EndsWith(".md")) return meta;

            var metaPath = string.Format("{0}{1}", md.FullName.Substring(0, md.FullName.Length - 2), "xml");

            if (!File.Exists(metaPath)) return meta;

            var xdoc = XDocument.Parse(File.ReadAllText(metaPath));

            var root = xdoc.Root;

            if (root == null) return meta;

            meta.PageTitleLinks = root.GetSafeAttribute("pageTitleLinks");
            meta.PageTitle = root.GetSafeAttribute("pageTitle");
            meta.MetaDescription = root.GetSafeAttribute("metaDescription");
            meta.Relevance = root.GetSafeAttribute("relevance");
            meta.Revision = root.GetSafeAttribute("revision");

            meta.Items = root.Descendants("item")
                .Select(item => new MdDataItem()
                {
                    Group = item.Attribute("group").Value, 
                    Alias = item.Attribute("alias").Value, 
                    Value = item.Attribute("value").Value
                }).ToList();

            return meta;
        }       
    }
}