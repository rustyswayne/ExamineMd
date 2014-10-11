namespace ExamineMd.Routing
{
    using System.Web;

    /// <summary>
    /// The not found handler.
    /// </summary>
    public class NotFoundHandler : IHttpHandler
    {
        /// <summary>
        /// Gets a value indicating whether is reusable.
        /// </summary>
        public bool IsReusable
        {
            get { return true; }
        }

        /// <summary>
        /// The process request.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void ProcessRequest(HttpContext context)
        {
            context.Response.StatusCode = 404;
        }
    }
}