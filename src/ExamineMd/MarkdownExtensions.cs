namespace ExamineMd
{
    using System.Web;
    using System.Web.Mvc;

    using Examine;

    using MarkdownSharp;

    /// <summary>
    /// Extension methods for Markdown.
    /// </summary>
    public static class MarkdownExtensions
    {
        /// <summary>
        /// The Markdown formatter.
        /// </summary>
        private static readonly Markdown MarkdownFormatter = new Markdown();

        /// <summary>
        /// Transforms a Markdown (md) formatted string as Html
        /// </summary>
        /// <param name="htmlHelper">
        /// The <see cref="HtmlHelper"/>
        /// </param>
        /// <param name="markdownContent">
        /// The markdown content
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this HtmlHelper htmlHelper, string markdownContent)
        {
            return string.IsNullOrEmpty(markdownContent) ? MvcHtmlString.Empty : 
                new MvcHtmlString(MarkdownFormatter.Transform(markdownContent).Trim());
        }

        /// <summary>
        /// Transforms an Examine document field string value to Html using Markdown Transformation.
        /// </summary>
        /// <param name="result">
        /// The <see cref="SearchResult"/>
        /// </param>
        /// <param name="fieldName">
        /// The Examine document field name referencing the Markdown string
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this SearchResult result, string fieldName)
        {
            return result.Fields.ContainsKey(fieldName)
                       ? new MvcHtmlString(MarkdownFormatter.Transform(result.Fields["fieldName"]).Trim())
                       : MvcHtmlString.Empty;
        }
    }
}