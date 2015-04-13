namespace ExamineMd.Routing
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Models;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Routing;

    /// <summary>
    /// The umbraco virtual node route handler.
    /// </summary>
    /// <remarks>
    /// https://github.com/Shandem/Articulate/blob/master/Articulate/UmbracoVirtualNodeRouteHandler.cs
    /// </remarks>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Reviewed. Suppression is OK here.")]
    public abstract class UmbracoVirtualNodeRouteHandler : IRouteHandler
    {
        /// <summary>
        /// The get http handler.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpHandler"/>.
        /// </returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
              
            var umbracoContext = UmbracoContext.Current;

            if (umbracoContext == null) return null;

            ////TODO: This is a huge hack - we need to publicize some stuff in the core
            ////TODO: publicize: ctor (or static method to create it), Prepared()
            var ensurePcr = new EnsurePublishedContentRequestAttribute(umbracoContext, "__virtualnodefinder__");

            var found = this.FindContent(requestContext, umbracoContext);
            if (found == null) return new NotFoundHandler();

            ////assign the node to our special token
            requestContext.RouteData.DataTokens["__virtualnodefinder__"] = found;

            ////this hack creates and assigns the pcr to the context
            ensurePcr.OnActionExecuted(new ActionExecutedContext { RequestContext = requestContext });

            ////allows inheritors to change the pcr
            this.PreparePublishedContentRequest(umbracoContext.PublishedContentRequest);

            ////create the render model
            var renderModel = new RenderModel(umbracoContext.PublishedContentRequest.PublishedContent, umbracoContext.PublishedContentRequest.Culture);

            ////assigns the required tokens to the request
            requestContext.RouteData.DataTokens.Add("umbraco", renderModel);
            requestContext.RouteData.DataTokens.Add("umbraco-doc-request", umbracoContext.PublishedContentRequest);
            requestContext.RouteData.DataTokens.Add("umbraco-context", umbracoContext);
           // umbracoContext.PublishedContentRequest.Prepare();


           // var values = requestContext.RouteData.Values;

           // values["action"] = this.UnDash(values["action"].ToString());
           // values["controller"] = this.UnDash(values["controller"].ToString());            
            
           //return new MvcHandler(requestContext);

            //Here we need to detect if a SurfaceController has posted
            var formInfo = GetFormInfo(requestContext);
            if (formInfo != null)
            {
                //TODO: We are using reflection for this but with the issue http://issues.umbraco.org/issue/U4-5710 fixed we 
                // probably won't need to use our own custom router

                //in order to allow a SurfaceController to work properly, the correct data token needs to be set, so we need to 
                // add a custom RouteDefinition to the collection
                var handle = Activator.CreateInstance("umbraco", "Umbraco.Web.Mvc.RouteDefinition", false, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, null, null, null);
                var def = handle.Unwrap();

                def.SetPropertyValue("PublishedContentRequest", umbracoContext.PublishedContentRequest);
                def.SetPropertyValue("ControllerName", requestContext.RouteData.GetRequiredString("controller"));
                def.SetPropertyValue("ActionName", requestContext.RouteData.GetRequiredString("action"));

                requestContext.RouteData.DataTokens["umbraco-route-def"] = def;

                try
                {
                    //First try to call this method as a static method (since it is a static method in umbraco 7.2)
                    // if that fails then we will call it with a non static instance since that is how it was pre-7.2)
                    return (IHttpHandler)typeof(RenderRouteHandler).CallStaticMethod("HandlePostedValues", requestContext, (object)formInfo);
                }
                catch (TargetException)
                {
                    var rrh = new RenderRouteHandler(ControllerBuilder.Current.GetControllerFactory());
                    return (IHttpHandler)rrh.CallMethod("HandlePostedValues", requestContext, (object)formInfo);
                }
            }

            return new MvcHandler(requestContext);

        }

        /// <summary>
        /// The find content.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <param name="umbracoContext">
        /// The umbraco context.
        /// </param>
        /// <returns>
        /// The <see cref="IPublishedContent"/>.
        /// </returns>
        protected abstract IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext);

        /// <summary>
        /// The prepare published content request.
        /// </summary>
        /// <param name="publishedContentRequest">
        /// The published content request.
        /// </param>
        protected virtual void PreparePublishedContentRequest(PublishedContentRequest publishedContentRequest)
        {
        }

        /// <summary>
        /// Check the request to see if a SurfaceController has posted any data via a SurfaceController
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        /// <remarks>
        /// This uses reflection to call the underlying logic that is done in the Umbraco core, this won't be necessary when this
        /// issue is fixed: http://issues.umbraco.org/issue/U4-5710 since we don't have to use our own route handlers.
        /// </remarks>
        private dynamic GetFormInfo(RequestContext requestContext)
        {
            var result = typeof(RenderRouteHandler).CallStaticMethod("GetFormInfo", requestContext);
            return result;
        }

        /// <summary>
        /// Converts some/thing-here urls to Some/ThingHere
        /// </summary>
        /// <param name="path">
        /// The path from which to remove dashes
        /// </param>
        /// <returns>
        /// The undashed path.
        /// </returns>
        private string UnDash(string path)
        {
            if (path.Length == 0)
                return path;

            var sb = new StringBuilder();

            sb.Append(char.ToUpperInvariant(path[0]));

            for (int i = 1; i < path.Length; i++)
            {
                if (path[i] == '-')
                {
                    if (i + 1 < path.Length)
                    {
                        sb.Append(char.ToUpperInvariant(path[i + 1]));
                        i++;
                    }
                }
                else
                {
                    sb.Append(path[i]);
                }
            }

            return sb.ToString();
        }
    }
}
