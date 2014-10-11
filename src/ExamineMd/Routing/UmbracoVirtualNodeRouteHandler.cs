namespace ExamineMd.Routing
{
    using System.Diagnostics.CodeAnalysis;
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
            umbracoContext.PublishedContentRequest.ConfigureRequest();


            var values = requestContext.RouteData.Values;

            values["action"] = this.UnDash(values["action"].ToString());
            values["controller"] = this.UnDash(values["controller"].ToString());            
            
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
