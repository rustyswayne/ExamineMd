namespace ExamineMd.Models
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
    public class VirtualMarkdownListing : BaseViewModel, IVirtualMarkdownListing
    {
        /// <summary>
        /// The document route.
        /// </summary>
        private static readonly string ListingRoute = Constants.MarkdownListingRoute.EnsureStartsAndEndsWith('/');

        /// <summary>
        /// The Documents.
        /// </summary>
        private readonly IEnumerable<IPublishedContent> _children; 

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownListing"/> class.
        /// </summary>
        /// <param name="content">
        /// The <see cref="IPublishedContent"/>.
        /// </param>
        /// <param name="parent">
        /// The parent node (can be virtual)
        /// </param>
        /// <param name="children">
        /// The collection of <see cref="IPublishedContent"/> children
        /// </param>
        public VirtualMarkdownListing(IPublishedContent content, IPublishedContent parent, IEnumerable<IPublishedContent> children)
            : base(content, parent)
        {
            Mandate.ParameterNotNull(children, "children");

            _children = children;
        }

        /// <summary>
        /// Gets or sets the brief summary of the contents.  Applies to listings only.
        /// </summary>
        public string Brief { get; set; }

        /// <summary>
        /// Gets or sets the max list count.
        /// </summary>
        public int MaxListCount { get; set; }

        /// <summary>
        /// Gets the starting path.
        /// </summary>
        public IMdPath StartingPath 
        {
            get
            {
                return new MdPath(RootContent.GetPropertyValue<string>("startingPath").EnsureBackSlashes());      
            } 
        }

        #region IPublishedContent

        /// <summary>
        /// Gets the url.
        /// </summary>
        /// <remarks>
        /// This is a virtual path not associated with an actual Umbraco content node. 
        /// </remarks>
        public override string Url
        {
            get
            {
                var routeIndex = Content.Url.IndexOf(ListingRoute, StringComparison.InvariantCultureIgnoreCase);
                if (routeIndex <= 0)
                {
                    return Content.Url.EnsureNotEndsWith('/') + ListingRoute.EnsureNotEndsWith('/') + MdPath.Value.EnsureForwardSlashes();
                }

                return this.Content.Url.Substring(0, this.Content.Url.IndexOf(ListingRoute, StringComparison.InvariantCultureIgnoreCase) + ListingRoute.Length).EnsureNotEndsWith('/') + MdPath.Value.EnsureForwardSlashes();
            }
        }

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
                return _children;
            }
        }

        #endregion
    }
}