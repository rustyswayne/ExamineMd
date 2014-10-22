namespace ExamineMd.Models
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    /// <summary>
    /// The ExamineMd VirtualMarkdownBase.
    /// </summary>
    public class VirtualMarkdownBase : PublishedContentWrapped, IVirtualMarkdownBase
    {
        /// <summary>
        /// The parent listing.
        /// </summary>
        private readonly IPublishedContent _parent;

        /// <summary>
        /// The start path set in the Umbraco content
        /// </summary>
        private string _startPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownBase"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="parent">
        /// The parent node (can be a virtual node)
        /// </param>
        /// <param name="path">
        /// The relative file system path
        /// </param>
        public VirtualMarkdownBase(IPublishedContent content, IPublishedContent parent, IMdPath path)
            : base(content)
        {
            Mandate.ParameterNotNull(parent, "parent");

            _parent = parent;
            
            MdPath = path;

            this.Initialize();
        }


        /// <summary>
        /// Gets the root content.
        /// </summary>
        public IPublishedContent RootContent
        {
            get
            {
                return Content.AncestorOrSelf("ExamineMd");
            } 
        }

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

        /// <summary>
        /// Gets or sets the HTML Page Title.
        /// </summary>
        public string PageTitle { get; set; }

        /// <summary>
        /// Gets or sets the meta description.
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// Gets or sets the page's headline.
        /// </summary>
        public string HeadleLine { get; set; }

        /// <summary>
        /// Gets a value indicating whether is a document.
        /// </summary>
        public bool IsDocument
        {
            get
            {
                return MdPath.IsDocument; 
            }            
        }

        #region IPublishedContent

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
            get { return _parent; }
        }


        /// <summary>
        /// Gets the level.
        /// </summary>
        public override int Level
        {
            get
            {
                return Content.Level + 1;
            }
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
        /// Gets the url.
        /// </summary>
        /// <remarks>
        /// This is a virtual path not associated with an actual Umbraco content node. 
        /// </remarks>
        public override string Url
        {
            get
            {
                return MdPath.IsDocument
                           ? string.Format(
                               "{0}{1}{2}",
                               RootContent.Url.EnsureNotEndsWith('/'),
                               _startPath,
                               PathHelper.GetFileNameForUrl(MdPath.FileName))
                           : string.Format("{0}{1}", RootContent.Url.EnsureNotEndsWith('/'), _startPath);
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the examine md path.
        /// </summary>
        internal IMdPath MdPath { get; set; }

        /// <summary>
        /// Initializes the object.
        /// </summary>
        private void Initialize()
        {
            _startPath = MdPath.Value.Replace(StartingPath.Value, string.Empty).EnsureForwardSlashes().EnsureEndsWith('/');
        }
    }
}