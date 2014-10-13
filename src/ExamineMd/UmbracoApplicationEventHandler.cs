namespace ExamineMd
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using Examine.LuceneEngine.Providers;

    using ExamineMd.Controllers.Api;
    using ExamineMd.Routing;

    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;
    using Umbraco.Web.UI.JavaScript;

    /// <summary>
    /// Registers ExamineMd specific Umbraco application event handlers
    /// </summary>
    public class UmbracoApplicationEventHandler : ApplicationEventHandler
    {
        /// <summary>
        /// Overrides for Umbraco application starting.
        /// </summary>
        /// <param name="umbracoApplication">
        /// The Umbraco application.
        /// </param>
        /// <param name="applicationContext">
        /// The application context.
        /// </param>
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //// Add custom routes
            LogHelper.Info<UmbracoApplicationEventHandler>("ExamineMd - Adding Custom Routes");

            ExamineMdRoutes.MapRoutes(RouteTable.Routes, UmbracoContext.Current.ContentCache);

            ServerVariablesParser.Parsing += ServerVariablesParserOnParsing;
        }

        /// <summary>
        /// Handles the ServerVariables Parsing to add base path.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        static void ServerVariablesParserOnParsing(object sender, Dictionary<string, object> dictionary)
        {
            if (HttpContext.Current == null) throw new InvalidOperationException("HttpContext is null");
            var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));
            
            dictionary.Add(
                "examineMd", 
                new Dictionary<string, object>
                {
                    { "examindMdPropertyEditorsBaseUrl", urlHelper.GetUmbracoApiServiceBaseUrl<ExamineMdPropertyEditorsController>(controller => controller.GetAllPaths()) }
                });
        }
    }
}