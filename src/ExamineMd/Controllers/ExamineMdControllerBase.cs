using Umbraco.Web;

namespace ExamineMd.Controllers
{
    using System.Runtime.InteropServices.WindowsRuntime;
    using System.Web.Mvc;

    using ExamineMd.Factories;
    using ExamineMd.Search;

    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;

    /// <summary>
    /// The ExamineMd view controller base class.
    /// </summary>
    public abstract class ExamineMdControllerBase : RenderMvcController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExamineMdControllerBase"/> class.
        /// </summary>
        protected ExamineMdControllerBase()
        {
            VirtualContentFactory = new VirtualContentFactory(new MarkdownQuery());
        }

        /// <summary>
        /// Gets the <see cref="VirtualContentFactory"/>.
        /// </summary>
        internal VirtualContentFactory VirtualContentFactory { get; private set; }

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

        /// <summary>
        /// The gets the starting path default queries.
        /// </summary>
        /// <param name="model">
        /// The model.
        /// </param>
        /// <returns>
        /// The starting path.
        /// </returns>
        protected static string GetSafeStartPath(IRenderModel model)
        {
            return model.Content.HasProperty("startingPath") && model.Content.HasValue("startingPath")
                       ? model.Content.GetPropertyValue<string>("startingPath")
                       : "/";
        }
    }
}