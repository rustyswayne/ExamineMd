namespace ExamineMd.Controllers.Api
{
    using System.Collections.Generic;
    using System.Linq;

    using ExamineMd.Search;

    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;

    /// <summary>
    /// The ExamineMdPropertyEditorsController.
    /// </summary>
    [PluginController("ExamineMd")]
    public class ExamineMdPropertyEditorsController : UmbracoApiController
    {
        /// <summary>
        /// The get all paths.
        /// </summary>
        /// <returns>
        /// The collection of paths.
        /// </returns>
        public IEnumerable<string> GetAllPaths()
        {
            return new MarkdownQuery().Paths.GetAll().Select(x => x.Value.EnsureForwardSlashes());
        }
    }
}