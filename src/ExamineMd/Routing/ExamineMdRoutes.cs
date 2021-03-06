﻿namespace ExamineMd.Routing
{
    using System.Linq;
    using System.Web.Routing;

    using Umbraco.Core.Logging;
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
            
            var examineMdNodes = umbracoCache.GetByXPath("//ExamineMd").ToArray();

            if (!examineMdNodes.Any()) return;

            using (routes.GetWriteLock())
            {
                var groups = examineMdNodes.GroupBy(x => RouteCollectionExtensions.RoutePathFromNodeUrl(x.Url));

                foreach (var group in groups)
                {
                    var groupHash = group.Key.GetHashCode();

                    var examineMdNode = group.First();

                    RemoveExisting(routes, new[] { "examinemd_" + groupHash });

                    var routeName = "examinemd_" + groupHash;
                    var routeUrl = examineMdNode.Url.EnsureNotStartsOrEndsWith('/') + "/{*path}";

                    LogHelper.Info(typeof(ExamineMdRoutes), string.Format("{0}, {1}", routeName, routeUrl));

                    routes.MapUmbracoRoute(
                    routeName,
                        routeUrl,
                        new { controller = "ExamineMd", action = "Index" },
                        new UmbracoVirtualNodeByIdRouteHandler(examineMdNode.Id));
                }
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
        public static void RemoveExisting(RouteCollection routes, params string[] names)
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