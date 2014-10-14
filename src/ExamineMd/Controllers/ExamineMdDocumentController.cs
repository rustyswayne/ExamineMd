namespace ExamineMd.Controllers
{
    using System.Web.Mvc;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using umbraco.cms.businesslogic.datatype.controls;

    using Umbraco.Core;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// The ExamineMdDocumentController is responsible for rendering ExamineMd file store markdown content.
    /// </summary>
    [PluginController("ExamineMd")]
    public class ExamineMdDocumentController : ExamineMdControllerBase
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
            if (path == null) return RenderView(model, null);

            return RenderView(model, path);
        }


        /// <summary>
        /// Responsible for rendering the view.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="url">The virtual url of the document</param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RenderView(IRenderModel model, string url)
        {
            var virtualContent = VirtualContentFactory.BuildDocument(model, url);

            return this.View(PathHelper.GetViewPath("Document"), virtualContent);
        }
    }
}