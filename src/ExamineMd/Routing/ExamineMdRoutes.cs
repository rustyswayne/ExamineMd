namespace ExamineMd.Routing
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Umbraco.Core;
    using Umbraco.Web.PublishedCache;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// Manages ExamineMd MVC routes.
    /// </summary>
    public static class ExamineMdRoutes
    {
        /// <summary>
        /// Does the actual work of mapping the MVC route.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        /// <param name="umbracoCache">
        /// The Umbraco cache file.
        /// </param>
        public static void MapRoutes(RouteCollection routes, ContextualPublishedCache umbracoCache)
        {
            // For this version there I'm assuming only one node but it'd be cool if multiple nodes 
            // could be used to start at various points in the file store tree.
            var examineMdNode = umbracoCache.GetByXPath("//ExamineMd").FirstOrDefault();
            
            if (examineMdNode == null) return;

            var documentPath = Constants.MarkdownDocumentRoute.SafeEncodeUrlSegments().EnsureNotStartsWith('/').EnsureEndsWith('/');
            var listingPath = Constants.MarkdownListingRoute.SafeEncodeUrlSegments().EnsureNotStartsWith('/').EnsureEndsWith('/');

            RemoveExisting(routes, new[] { "examinemd_markdown", "examinemd_listing" });

            routes.MapUmbracoRoute(
            "examinemd-markdown",
                documentPath + "/{*path}",
                new { controller = "ExamineMdDocumentFile", action = "Index", path = UrlParameter.Optional },
                new UmbracoVirtualNodeByIdRouteHandler(examineMdNode.Id));
        }

        /// <summary>
        /// Removes existing routes
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        /// <param name="names">
        /// The names.
        /// </param>
        private static void RemoveExisting(RouteCollection routes, params string[] names)
        {
            foreach (var name in names)
            {
                var r = routes[name];
                if (r != null)
                {
                    routes.Remove(r);
                }
            }
        }
    } 
}