namespace ExamineMd.Controllers
{
    using System.Web.Mvc;

    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    [PluginController("ExamineMd")]
    public class ExamineMdController : RenderMvcController
    {

        /// <summary>
        /// Declare new Index action with optional page number
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        public override ActionResult Index(RenderModel model)
        {
            return RenderView(model);
        }

        private ActionResult RenderView(IRenderModel model)
        {
            return this.View(PathHelper.GetViewPath("ExamineMd"), model);
        }
    }
}