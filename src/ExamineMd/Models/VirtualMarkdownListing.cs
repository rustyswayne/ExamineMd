﻿namespace ExamineMd.Models
{
    using System;
    using System.Collections.Generic;

    using MarkdownDeep;

    using umbraco;

    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;

    /// <summary>
    /// Represents a VirtualMarkdownListing.
    /// </summary>
    public class VirtualMarkdownListing : VirtualMarkdownBase, IVirtualMarkdownListing
    {

        /// <summary>
        /// The Documents.
        /// </summary>
        private readonly Lazy<IEnumerable<IPublishedContent>> _children;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownListing"/> class.
        /// </summary>
        /// <param name="content">
        /// The <see cref="IPublishedContent"/>.
        /// </param>
        /// <param name="parent">
        /// The parent node (can be virtual)
        /// </param>
        /// <param name="path">The <see cref="IMdPath"/> to this virtual content</param>
        /// <param name="children">
        /// The collection of <see cref="IPublishedContent"/> children
        /// </param>
        public VirtualMarkdownListing(IPublishedContent content, IPublishedContent parent, IMdPath path, Func<IPublishedContent, string, IEnumerable<IPublishedContent>> children)
            : base(content, parent, path)
        {
            Mandate.ParameterNotNull(children, "children");

            _children = new Lazy<IEnumerable<IPublishedContent>>(() => children(RootContent, path.Key));
        }

        /// <summary>
        /// Gets or sets the brief summary of the contents.  Applies to listings only.
        /// </summary>
        public string Brief { get; set; }

        /// <summary>
        /// Gets or sets the max list count.
        /// </summary>
        public int MaxListCount { get; set; }

        #region IPublishedContent

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <remarks>
        /// This is a virtual node so the Key does not correspond to Umbraco content
        /// </remarks>
        public override int Id
        {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// Gets the Umbraco Content Name.
        /// </summary>
        public override string Name
        {
            get
            {
                return UrlName.AsFormattedTitle();
            }
        }

        /// <summary>
        /// Gets the Umbraco UrlName.
        /// </summary>
        public override string UrlName
        {
            get
            {
                if (MdPath.IsRootPath()) return Content.Name;

                var lastIndex = MdPath.Value.EnsureForwardSlashes().LastIndexOf('/') + 1;
                
                return MdPath.Value.Substring(lastIndex, MdPath.Value.Length - lastIndex);
            }
        }

        /// <summary>
        /// Gets the Umbraco DocumentTypeAlias.
        /// </summary>
        public override string DocumentTypeAlias
        {
            get { return Constants.ContentTypes.ExamineMdMarkdownListing; }
        }


        /// <summary>
        /// Gets the collection of <see cref="IPublishedContent"/> children
        /// </summary>
        public override IEnumerable<IPublishedContent> Children
        {
            get
            {
                return _children.Value;
            }
        }

        #endregion
    }
}