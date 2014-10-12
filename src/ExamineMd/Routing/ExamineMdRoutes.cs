namespace ExamineMd.Routing
{
    using System.Linq;
    using System.Web.Routing;

    using Umbraco.Core;
    using Umbraco.Web.PublishedCache;

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
            // there can be only one
            var examineMdNode = umbracoCache.GetByXPath("//ExamineMd").FirstOrDefault();
            var rootPath = PathHelper.GetRootRoute().SafeEncodeUrlSegments().EnsureNotEndsWith('/').EnsureEndsWith('/');

            if (examineMdNode == null) return;

            using (routes.GetWriteLock())
            {
                RemoveExisting(routes, new[] { "examinemd_markdown", "examinemd_lists" });

                routes.MapUmbracoRoute(
                "examinemd_markdown",
                rootPath + "{*path}",
                new { controller = "ExamineMd", action = "Index" },
                new UmbracoVirtualNodeByIdRouteHandler(examineMdNode.Id));
            }
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