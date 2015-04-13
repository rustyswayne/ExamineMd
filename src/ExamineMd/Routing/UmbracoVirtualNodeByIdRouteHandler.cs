namespace ExamineMd.Routing
{
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Routing;

    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Routing;

    /// <summary>
    /// For MVC Routes, taken from https://github.com/Shandem/Articulate/blob/master/Articulate/UmbracoVirtualNodeByIdRouteHandler.cs
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed. Suppression is OK here.")]
    public class UmbracoVirtualNodeByIdRouteHandler : UmbracoVirtualNodeRouteHandler
    {
        /// <summary>
        /// The real node id.
        /// </summary>
        private readonly int _realNodeId;

        /// <summary>
        /// Initializes a new instance of the <see cref="UmbracoVirtualNodeByIdRouteHandler"/> class.
        /// </summary>
        /// <param name="realNodeId">
        /// The real node id.
        /// </param>
        public UmbracoVirtualNodeByIdRouteHandler(int realNodeId)
        {
            this._realNodeId = realNodeId;
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
        protected sealed override IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext)
        {
            var content = umbracoContext.ContentCache.GetById(this._realNodeId);
            if (content == null) return null;

            return this.FindContent(requestContext, umbracoContext, content);
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
        /// <param name="baseContent">
        /// The base content.
        /// </param>
        /// <returns>
        /// The <see cref="IPublishedContent"/>.
        /// </returns>
        protected virtual IPublishedContent FindContent(RequestContext requestContext, UmbracoContext umbracoContext, IPublishedContent baseContent)
        {
            return baseContent;
        }

        //NOTE: This is the manual way we could assign culture this but I think there's more logic for edge case scenarios in Umbraco's Prepare method.
        // I've just left this code here as an example
        protected override void PreparePublishedContentRequest(PublishedContentRequest publishedContentRequest)
        {
            //if (_hostsAndIds.Any(x => x.Item2 == publishedContentRequest.PublishedContent.Parent.Id))
            //{
            //    var hostAndId = _hostsAndIds.Single(x => x.Item2 == publishedContentRequest.PublishedContent.Parent.Id);
            //    var domain = Domain.GetDomain(hostAndId.Item1);
            //    publishedContentRequest.Culture = new CultureInfo(domain.Language.CultureAlias);
            //}

            base.PreparePublishedContentRequest(publishedContentRequest);

        }
    }
}
