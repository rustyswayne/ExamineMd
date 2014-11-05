namespace ExamineMd.Factories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;

    /// <summary>
    /// The virtual content factory.
    /// </summary>
    public class VirtualContentFactory
    {
        /// <summary>
        /// The <see cref="IMarkdownQuery"/>.
        /// </summary>
        private readonly IMarkdownQuery _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualContentFactory"/> class.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        public VirtualContentFactory(IMarkdownQuery query)
        {
            Mandate.ParameterNotNull(query, "query");

            _query = query;
        }

        /// <summary>
        /// Builds <see cref="IVirtualMarkdownBase"/>.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="IVirtualMarkdownBase"/>.
        /// </returns>
        public IVirtualMarkdownBase Build(IRenderModel model, string url)
        {

            url = GetStartingPath(model.Content).EnsureForwardSlashes().EnsureNotEndsWith('/') + url;
            var path = _query.Paths.GetByUrl(url);

            return Build(model, path);
        }

        /// <summary>
        /// Builds <see cref="IVirtualMarkdownBase"/>.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="path">
        /// The <see cref="IMdPath"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IVirtualMarkdownBase"/>.
        /// </returns>
        public IVirtualMarkdownBase Build(IRenderModel model, IMdPath path)
        {
            if (path.IsDocument)
            {
                var md = _query.Files.Get(path.Key);
                return this.BuildDocument(model, md);
            }

            return this.BuildListing(model, path);
        }

        /// <summary>
        /// Builds a <see cref="IVirtualMarkdownDocument"/>.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="md">
        /// The md.
        /// </param>
        /// <returns>
        /// The <see cref="IVirtualMarkdownDocument"/>.
        /// </returns>
        public IVirtualMarkdownDocument BuildDocument(IRenderModel model, IMdFile md)
        {
            var parent = md.Path.IsRootPath() || GetStartingPath(model.Content).Contains(md.Path.Value) ? 
                model.Content.Parent : 
                BuildListing(model, md.Path.ParentPath());

            var document = new VirtualMarkdownDocument(model.Content, parent, md)
            {
                HeadleLine = md.Title,
                PageTitle = string.IsNullOrEmpty(md.MetaData.PageTitle) ? md.Title : md.MetaData.PageTitle,
                MetaDescription = string.IsNullOrEmpty(md.MetaData.MetaDescription) ? string.Empty : md.MetaData.MetaDescription,
                MdPath = md.Path
            };

            return document;
        }

        /// <summary>
        /// Builds a <see cref="IVirtualMarkdownListing"/>.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IVirtualMarkdownListing"/>.
        /// </returns>
        public IVirtualMarkdownListing BuildListing(IRenderModel model, IMdPath path)
        {
            var parent = path.IsRootPath() || GetStartingPath(model.Content).Contains(path.Value)
                             ? model.Content.Parent
                             : this.Build(model, path.ParentPath());

            return new VirtualMarkdownListing(model.Content, parent, path, GetChildren);
        }

        /// <summary>
        /// Gets a list of virtual children
        /// </summary>
        /// <param name="content">The root content</param>
        /// <param name="pathKey">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        internal IEnumerable<IPublishedContent> GetChildren(IPublishedContent content, string pathKey)
        {
            var contents = new List<IVirtualMarkdownBase>();

            // Directories
            var directories = _query.Paths.GetByPathKey(pathKey).Where(x => !x.IsRootPath()).ToArray();
            if (directories.Any())
            {
                contents.AddRange(directories.Select(x => this.BuildListing(new RenderModel(content), x)));
            }

            // Files
            var files = _query.Files.GetByPathKey(pathKey).ToArray();
            if (files.Any())
            {
                contents.AddRange(files.Select(x => this.BuildDocument(new RenderModel(content), x)));
            }

            return contents;
        }

        private static string GetStartingPath(IPublishedContent content)
        {
            var startPathing = content.GetPropertyValue<string>("startingPath");
            return string.IsNullOrEmpty(startPathing) ? "\\" : startPathing.EnsureBackSlashes();
        }
    }
}