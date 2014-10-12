namespace ExamineMd.Controllers
{
    using System.Web.Mvc;

    using AutoMapper.Internal;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using Umbraco.Core;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// The examine md controller.
    /// </summary>
    [PluginController("ExamineMd")]
    public class ExamineMdController : RenderMvcController
    {
        /// <summary>
        /// The <see cref="IMarkdownQuery"/>.
        /// </summary>
        private readonly IMarkdownQuery _markdownQuery = new MarkdownQuery();

        /// <summary>
        /// Declare new Index action with optional page number
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
            return RenderView(model, null);
        }

        /// <summary>
        /// The default method
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

            path = path.EnsureStartsWith('/');

            var md = _markdownQuery.GetByUrl(path);

            return RenderView(model, md);
        }


        /// <summary>
        /// Responsible for rendering the view.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="md">The <see cref="IMdFile"/></param>
        /// <returns>
        /// The <see cref="ActionResult"/>.
        /// </returns>
        private ActionResult RenderView(IRenderModel model, IMdFile md)
        {
            var virtualContent = new MarkdownVirtualContent(model.Content, md);

            return this.View(PathHelper.GetViewPath("ExamineMd"), virtualContent);
        }
    }
}