namespace ExamineMd.Controllers
{
    using System;
    using System.Web.Mvc;

    using ExamineMd.Models;

    using Umbraco.Core;
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
            return RenderView(model, path);
        }

        /// <summary>
        /// Responsible for rendering the view.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="path">
        /// The collection of documents to list
        /// </param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RenderView(IRenderModel model, string path)
        {            
            var url = PathHelper.ValidateDocumentPath(path).EnsureForwardSlashes().EnsureStartsWith('/');
         
            var virtualContent = VirtualContentFactory.Build(model, url);

            var viewName = virtualContent.IsDocument ? "Document" : "Listing";

            return this.View(PathHelper.GetViewPath(viewName), virtualContent);
        }
    }
}