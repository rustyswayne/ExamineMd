namespace ExamineMd.Routing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;
    using System.Web.Routing;

    /// <summary>
    /// Helpers for MVC routes taken from https://github.com/Shandem/Articulate/blob/master/Articulate/RouteCollectionExtensions.cs
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public static class RouteCollectionExtensions
    {
        /// <summary>
        /// Maps a custom Umbraco route.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="defaults">
        /// The defaults.
        /// </param>
        /// <param name="virtualNodeHandler">
        /// The virtual node handler.
        /// </param>
        /// <param name="constraints">
        /// The constraints.
        /// </param>
        /// <param name="namespaces">
        /// The namespaces.
        /// </param>
        /// <returns>
        /// The <see cref="Route"/>.
        /// </returns>
        public static Route MapUmbracoRoute(
            this RouteCollection routes,
            string name, 
            string url, 
            object defaults, 
            UmbracoVirtualNodeRouteHandler virtualNodeHandler,
            object constraints = null, 
            string[] namespaces = null)
        {
            var route = RouteTable.Routes.MapRoute(name, url, defaults, constraints, namespaces);
            route.RouteHandler = virtualNodeHandler;
            route.AddRouteNameToken(name);
            return route;
        }


        /// <summary>
        /// Add the route name to the data tokens so we can search on it later - unfortunately the RouteCollection doesn't let
        /// use query names directly.
        /// </summary>
        /// <param name="route"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        internal static Route AddRouteNameToken(this Route route, string name)
        {
            if (route.DataTokens == null)
            {
                route.DataTokens = new RouteValueDictionary();
            }
            route.DataTokens["__RouteName"] = name;
            return route;
        }

        /// <summary>
        /// Returns a route path from a given node's URL since a node's Url might contain a domain which we can't use in our routing.
        /// </summary>
        /// <param name="routePath"></param>
        /// <returns></returns>
        internal static string RoutePathFromNodeUrl(string routePath)
        {
            Uri result;
            return Uri.TryCreate(routePath, UriKind.Absolute, out result)
                ? result.PathAndQuery.TrimStart('/')
                : routePath.TrimStart('/');
        }
    }
}
