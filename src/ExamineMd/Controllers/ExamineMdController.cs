namespace ExamineMd.Controllers
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// The examine md controller.
    /// </summary>
    [PluginController("ExamineMd")]
    public class ExamineMdController : ExamineMdControllerBase
    {

        /// <summary>
        /// Declare new Index action with optional path
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        public ActionResult Index(RenderModel model, string path)
        {

            var startPath = GetSafeStartPath(model);

            var docs = (string.IsNullOrEmpty(path) ? MarkdownQuery.List(startPath) : MarkdownQuery.List(path))
                .Select(x => new VirtualMarkdownDocument(model.Content, x));

            return RenderView(model, docs);
        }

        /// <summary>
        /// Responsible for rendering the view.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="documents">
        /// The collection of documents to list
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RenderView(IRenderModel model, IEnumerable<IVirtualMarkdownDocument> documents)
        {
            var virtualContent = new VirtualMarkdownListing(model.Content, documents);

            return this.View(PathHelper.GetViewPath("Listing"), virtualContent);
        }

        /// <summary>
        /// The gets the starting path default queries.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The starting path.
        /// </returns>
        private static string GetSafeStartPath(IRenderModel model)
        {
            return model.Content.HasProperty("startingPath") && model.Content.HasValue("startingPath")
                       ? model.Content.GetPropertyValue<string>("startingPath")
                       : "/";
        }

    }
}