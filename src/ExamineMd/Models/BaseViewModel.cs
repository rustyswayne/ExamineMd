namespace ExamineMd.Models
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    /// <summary>
    /// The ExamineMd BaseViewModel.
    /// </summary>
    public class BaseViewModel : PublishedContentWrapped, IBaseViewModel
    {
        /// <summary>
        /// The parent listing.
        /// </summary>
        private readonly IPublishedContent _parent;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="parent">
        /// The parent node (can be a virtual node)
        /// </param>
        public BaseViewModel(IPublishedContent content, IPublishedContent parent)
            : base(content)
        {
            Mandate.ParameterNotNull(parent, "parent");
            _parent = parent;
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

        #endregion

        /// <summary>
        /// Gets or sets the examine md path.
        /// </summary>
        internal IMdPath MdPath { get; set; }
    }
}