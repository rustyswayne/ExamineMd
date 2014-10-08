namespace ExamineMd
{
    using System.Web;
    using System.Web.Mvc;

    using Examine;

    using ExamineMd.Models;

    using MarkdownDeep;

    /// <summary>
    /// Extension methods for Markdown.
    /// </summary>
    public static class MarkdownExtensions
    {
        /// <summary>
        /// The Markdown formatter.
        /// </summary>
        private static readonly Markdown MarkdownFormatter = new Markdown()
                                                                 {
                                                                     ExtraMode = true,
                                                                     SafeMode = false
                                                                 };

        /// <summary>
        /// Transforms a Markdown (md) formatted string as Html
        /// </summary>
        /// <param name="htmlHelper">
        /// The <see cref="HtmlHelper"/>
        /// </param>
        /// <param name="md">
        /// The markdown content
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this HtmlHelper htmlHelper, string md)
        {
            return string.IsNullOrEmpty(md) ? MvcHtmlString.Empty : 
                new MvcHtmlString(MarkdownFormatter.Transform(md).Trim());
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

        /// <summary>
        /// Transforms the file body text into Html using Markdown Transformation
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString Markdown(this IMdFile file)
        {
            return string.IsNullOrEmpty(file.Body) ? MvcHtmlString.Empty : new MvcHtmlString(MarkdownFormatter.Transform(file.Body));
        }
    }
}