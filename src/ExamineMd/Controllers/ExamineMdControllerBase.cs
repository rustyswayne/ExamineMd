namespace ExamineMd.Controllers
{
    using System.Web.Mvc;

    using ExamineMd.Search;

    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// The ExamineMd view controller base class.
    /// </summary>
    public abstract class ExamineMdControllerBase : RenderMvcController
    {
        /// <summary>
        /// The <see cref="IMarkdownQuery"/>.
        /// </summary>
        private readonly IMarkdownQuery _markdownQuery;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamineMdControllerBase"/> class.
        /// </summary>
        protected ExamineMdControllerBase()
        {
            _markdownQuery = new MarkdownQuery();
        }

        /// <summary>
        /// Gets the <see cref="IMarkdownQuery"/>.
        /// </summary>
        protected IMarkdownQuery MarkdownQuery
        {
            get
            {
                return _markdownQuery;
            }
        }

        /// <summary>
        /// This method is abandoned but must have an override.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        [NonAction]
        public override ActionResult Index(RenderModel model)
        {
            return null;
        }
    }
}