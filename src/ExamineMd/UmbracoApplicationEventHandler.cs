namespace ExamineMd
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using ExamineMd.Controllers.Api;
    using ExamineMd.Routing;

    using Umbraco.Core;
    using Umbraco.Core.Events;
    using Umbraco.Core.Logging;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
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
        [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:StaticElementsMustAppearBeforeInstanceElements", Justification = "Reviewed. Suppression is OK here.")]
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //// Add custom routes
            LogHelper.Info<UmbracoApplicationEventHandler>("ExamineMd - Adding Custom Routes");
            ExamineMdRoutes.MapRoutes(RouteTable.Routes, UmbracoContext.Current.ContentCache);
                       
            ContentService.Saved += ContentServiceOnSaved;
            ContentService.Deleted += ContentServiceOnDeleted;
            
            // Back office resources
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
        protected static void ServerVariablesParserOnParsing(object sender, Dictionary<string, object> dictionary)
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

        /// <summary>
        /// Handles the ContentService Saved event.
        /// Re-maps ExamineMd routes if another doctype is added
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="saveEventArgs">
        /// The save event args.
        /// </param>
        private static void ContentServiceOnSaved(IContentService sender, SaveEventArgs<IContent> saveEventArgs)
        {
            var contents = saveEventArgs.SavedEntities.Where(x => x.ContentType.Name.Equals("ExamineMd"));
            if (!contents.Any()) return;

            MapExamineMdRoutes();
        }


        private static void ContentServiceOnDeleted(IContentService sender, DeleteEventArgs<IContent> deleteEventArgs)
        {
            var contents = deleteEventArgs.DeletedEntities.Where(x => x.ContentType.Name.Equals("ExamineMd"));
            if (!contents.Any()) return;

            // TODO clear routes in the event there are none
            MapExamineMdRoutes();
        }

        /// <summary>
        /// Maps (or re-maps) ExamineMd Routes.
        /// </summary>
        private static void MapExamineMdRoutes()
        {
            ExamineMdRoutes.MapRoutes(RouteTable.Routes, UmbracoContext.Current.ContentCache);
        }
    }
}