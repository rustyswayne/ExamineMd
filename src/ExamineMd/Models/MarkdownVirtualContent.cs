namespace ExamineMd.Models
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// A virtual content page for displaying markdown.
    /// </summary>
    public class MarkdownVirtualContent : PublishedContentWrapped, IMarkdownVirtualContent
    {
        private string _pageTitle;

        private string _metaDescription;

        private string _url;


        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownVirtualContent"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="md">
        /// The md.
        /// </param>
        public MarkdownVirtualContent(IPublishedContent content, IMdFile md)
            : base(content)
        {
            Markdown = md ?? new MdFile()
                                 {
                                     Path = new MdPath(content.Url)
                                 };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownVirtualContent"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        internal MarkdownVirtualContent(IPublishedContent content)
            : this(content, null)
        {
        }

        /// <summary>
        /// Gets the HTML page title.
        /// </summary>
        public string PageTitle
        {
            get
            {
                return Markdown == null
                           ? Content.Name
                           : string.IsNullOrEmpty(Markdown.MetaData.PageTitle)
                                 ? Markdown.Title
                                 : Markdown.MetaData.PageTitle;
            }
        }

        /// <summary>
        /// Gets the meta description.
        /// </summary>
        public string MetaDescription
        {
            get
            {
                return Markdown == null
                    ? string.Empty
                    : string.IsNullOrEmpty(Markdown.MetaData.MetaDescription)
                    ? string.Empty
                    : Markdown.MetaData.MetaDescription;
            }
        }

        /// <summary>
        /// Gets the relevance.
        /// </summary>
        public string Relevance
        {
            get
            {
                return Markdown == null
                    ? string.Empty
                    : string.IsNullOrEmpty(Markdown.MetaData.Relevance)
                    ? string.Empty
                    : Markdown.MetaData.Relevance;
            }
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        public string Revision
        {
            get
            {
                return Markdown == null
                    ? string.Empty
                    : string.IsNullOrEmpty(Markdown.MetaData.Revision)
                    ? string.Empty
                    : Markdown.MetaData.Revision;
            }
        }

        /// <summary>
        /// Gets the <see cref="IMdFile"/> deserialized from the Examine index
        /// </summary>
        public IMdFile Markdown { get; private set; }

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
                return Markdown == null ?
                    base.Url.EnsureNotEndsWith('/') :
                    base.Url.EnsureNotEndsWith('/') + (Markdown.SearchableUrl() ?? UrlName);
            }
        }

        /// <summary>
        /// Gets the content type which is N/A - so is ALWAYS null
        /// </summary>
        public override PublishedContentType ContentType
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <remarks>
        /// This is always the <see cref="IPublishedContent"/> of the rendering node
        /// </remarks>
        public override IPublishedContent Parent
        {
            get { return Content; }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <remarks>
        /// This is a virtual node so the Id does not correspond to Umbraco content
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
                return string.IsNullOrEmpty(Markdown.Title) ? Content.Name : Markdown.Title;
            }
        }

        /// <summary>
        /// Gets the Umbraco UrlName.
        /// </summary>
        public override string UrlName
        {
            get
            {
                return (string.IsNullOrEmpty(Markdown.Title) ? Content.Name : Markdown.Title).ToLowerInvariant();
            }
        }

        /// <summary>
        /// Gets the Umbraco DocumentTypeAlias.
        /// </summary>
        public override string DocumentTypeAlias
        {
            get { return Constants.ExamineMdContentTypeAlias; }
        }

        /// <summary>
        /// Gets the document type id.
        /// </summary>
        public override int DocumentTypeId
        {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// Gets the Umbraco path.
        /// </summary>
        public override string Path
        {
            get { return Content.Path.EnsureEndsWith(',') + Id; }
        }

        /// <summary>
        /// Gets the Umbraco level.
        /// </summary>
        public override int Level
        {
            get { return Content.Level + 1; }
        }

        #endregion
    }
}