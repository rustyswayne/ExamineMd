namespace ExamineMd.Controllers.Api
{
    using System.Web.Http;

    using Examine;

    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;

    /// <summary>
    /// API Controller intended to be used as an endpoint of a web hook to rebuild the document index.
    /// </summary>
    [PluginController("ExamineMd")]
    public class RebuildIndexApiController : UmbracoApiController
    {
        /// <summary>
        /// The default method.
        /// </summary>
        /// <returns>
        /// A string saying OK =).
        /// </returns>
        [HttpGet]
        public string Index()
        {
            ExamineManager.Instance.IndexProviderCollection["ExamineMdIndexer"].RebuildIndex();

            return "OK";
        }
    }
}