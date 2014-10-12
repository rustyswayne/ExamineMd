namespace ExamineMd.Models
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    /// <summary>
    /// The ExamineMd BaseViewModel.
    /// </summary>
    public class BaseViewModel : PublishedContentWrapped, IBaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        public BaseViewModel(IPublishedContent content)
            : base(content)
        {
            HeadleLine = content.GetPropertyValue<string>("headline");
        }

        /// <summary>
        /// Gets the root content.
        /// </summary>
        public IPublishedContent RootContent
        {
            get
            {
                return Content;
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
    }
}