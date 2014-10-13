namespace ExamineMd.Models
{
    using System;
    using System.Globalization;

    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// A virtual content page for displaying markdown.
    /// </summary>
    public class VirtualMarkdownDocument : BaseViewModel, IVirtualMarkdownDocument
    {
        /// <summary>
        /// The document route.
        /// </summary>
        private static readonly string DocumentsRoute = Constants.MarkdownDocumentRoute.EnsureStartsAndEndsWith('/');

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownDocument"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="md">
        /// The <see cref="IMdFile"/>
        /// </param>
        public VirtualMarkdownDocument(IPublishedContent content, IMdFile md)
            : base(content)
        {
            Mandate.ParameterNotNull(md, "md");

            Markdown = md;

            this.Initialize();
        }

        /// <summary>
        /// Gets the relevance.
        /// </summary>
        public string Relevance
        {
            get
            {
                return string.IsNullOrEmpty(Markdown.MetaData.Relevance)
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
                return string.IsNullOrEmpty(Markdown.MetaData.Revision)
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
                //return Markdown.Path.Value.Equals("/", StringComparison.InvariantCultureIgnoreCase)
                //    ? Content.Url.EnsureNotEndsWith('/') + DocumentsRoute + UrlName
                //    : Content.Url.Substring(0, Content.Url.IndexOf(DocumentsRoute, StringComparison.InvariantCultureIgnoreCase)).EnsureNotEndsWith('/') + Markdown.SearchableUrl();

                var url = Content.Url;

                var routeIndex = Content.Url.IndexOf(DocumentsRoute, StringComparison.InvariantCultureIgnoreCase);

                if (routeIndex <= 0)
                {
                    url = Content.Url.EnsureNotEndsWith('/') + DocumentsRoute + UrlName;
                }
                else
                {
                    url = Content.Url.Substring(0, Content.Url.IndexOf(DocumentsRoute, StringComparison.InvariantCultureIgnoreCase) + DocumentsRoute.Length).EnsureNotEndsWith('/') + Markdown.SearchableUrl();
                }
                return url;
            }
        }

        /// <summary>
        /// Gets the content type which is N/A - so is ALWAYS null
        /// </summary>
        public override PublishedContentType ContentType
        {
            get { return null; }
        }

        ///// <summary>
        ///// Gets the parent.
        ///// </summary>
        ///// <remarks>
        ///// This is always the <see cref="IPublishedContent"/> of the rendering node
        ///// </remarks>
        ////// TODO - this should go to the parent list
        //public override IPublishedContent Parent
        //{
        //    get { return Content; }
        //}

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
                return Markdown.Title;
            }
        }

        /// <summary>
        /// Gets the Umbraco UrlName.
        /// </summary>
        public override string UrlName
        {
            get
            {
                var lastIndex = Markdown.SearchableUrl().LastIndexOf('/') + 1;
                return Markdown.SearchableUrl().Substring(lastIndex, Markdown.SearchableUrl().Length - lastIndex);
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

        /// <summary>
        /// Performs Initialization
        /// </summary>
        private void Initialize()
        {

            HeadleLine = Markdown.Title;

            PageTitle = string.IsNullOrEmpty(Markdown.MetaData.PageTitle)
                                 ? Markdown.Title
                                 : Markdown.MetaData.PageTitle;

            MetaDescription = string.IsNullOrEmpty(Markdown.MetaData.MetaDescription)
                                ? string.Empty
                                : Markdown.MetaData.MetaDescription;
        }
    }
}