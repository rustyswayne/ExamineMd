namespace ExamineMd.Factories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using Umbraco.Core.Models;
    using Umbraco.Web.Models;

    /// <summary>
    /// The virtual content factory.
    /// </summary>
    internal class VirtualContentFactory
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
        /// Builds a <see cref="IVirtualMarkdownDocument"/>
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="IVirtualMarkdownDocument"/>.
        /// </returns>
        public IVirtualMarkdownDocument BuildDocument(IRenderModel model, string url)
        {
            
            var md = _query.Files.GetByUrl(url);

            return BuildDocument(model, md);
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
            var parent = md.Path.IsRootPath() ? model.Content.Parent : BuildListing(model, md.Path.ParentPath());

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
            //if (path.IsRootPath()) return this.BuildRootListing(model);

            //var parent = model.Content.Parent;

            return new VirtualMarkdownListing(model.Content, model.Content.Parent, GetChildren(model, path));
        }

        private IVirtualMarkdownListing _rootListing;
        private IVirtualMarkdownListing BuildRootListing(IRenderModel model)
        {
            return _rootListing ?? new VirtualMarkdownListing(model.Content, model.Content.Parent, GetChildren(model, new MdPath("\\")));
        }

        /// <summary>
        /// Gets a list of virtual children
        /// </summary>
        /// <param name="model">The <see cref="IRenderModel"/></param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        internal IEnumerable<IPublishedContent> GetChildren(IRenderModel model, IMdPath path)
        {
            var contents = new List<IPublishedContent>();

            // Directories
            var directories = _query.Paths.GetByPathKey(path.Key).Where(x => !x.IsRootPath()).ToArray();
            if (directories.Any())
            {
                contents.AddRange(directories.Select(x => this.BuildListing(model, x)));
            }

            // Files
            var files = _query.Files.GetByPathKey(path.Key).ToArray();
            if (files.Any())
            {
                contents.AddRange(files.Select(x => this.BuildDocument(model, x)));
            }

            return contents;
        }
    }
}