namespace ExamineMd
{
    using System.Web.Routing;

    using ExamineMd.Routing;

    using Umbraco.Core;
    using Umbraco.Core.Logging;
    using Umbraco.Web;

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
        }
    }
}