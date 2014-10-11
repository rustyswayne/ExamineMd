namespace ExamineMd
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Controllers.Api;
    using Examine;
    
    using MarkdownDeep;
    using Models;
    
    /// <summary>
    /// Extension methods for MarkdownAsHtmlString.
    /// </summary>
    public static class MarkdownExtensions
    {
        ///// <summary>
        ///// The MarkdownAsHtmlString formatter.
        ///// </summary>
        //private static readonly Markdown MarkdownFormatter = new Markdown()
        //                                                         {
        //                                                             ExtraMode = true,
        //                                                             SafeMode = false
        //                                                         };


        /// <summary>
        /// Transforms a MarkdownAsHtmlString (md) formatted string as Html
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
        public static IHtmlString MarkdownHtmlString(this HtmlHelper htmlHelper, string md)
        {
            var formatter = GetMarkdownFormatter();

            return string.IsNullOrEmpty(md) ? MvcHtmlString.Empty : 
                new MvcHtmlString(formatter.Transform(md).Trim());
        }

        /// <summary>
        /// Transforms an Examine document field string value to Html using MarkdownAsHtmlString Transformation.
        /// </summary>
        /// <param name="result">
        /// The <see cref="SearchResult"/>
        /// </param>
        /// <param name="fieldName">
        /// The Examine document field name referencing the MarkdownAsHtmlString string
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString MarkdownAsHtmlString(this SearchResult result, string fieldName)
        {
            var formatter = GetMarkdownFormatter();

            return result.Fields.ContainsKey(fieldName)
                       ? new MvcHtmlString(formatter.Transform(result.Fields["fieldName"]).Trim())
                       : MvcHtmlString.Empty;
        }

        /// <summary>
        /// Transforms the file body text into Html using MarkdownAsHtmlString Transformation
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString BodyHtml(this IMdFile file)
        {
            var formatter = GetMarkdownFormatter();

            return string.IsNullOrEmpty(file.Body) ? MvcHtmlString.Empty : new MvcHtmlString(formatter.Transform(file.Body));
        }

        /// <summary>
        /// Converts the body to an Html string
        /// </summary>
        /// <param name="file">
        /// The file.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        /// <remarks>
        /// This is internally used by <see cref="SearchApiController"/> to minimize the response size.
        /// </remarks>
        internal static IMdFile ConvertBody(this IMdFile file)
        {
            file.Body = file.BodyHtml().ToHtmlString();
            return file;
        }

        /// <summary>
        /// The get markdown formatter.
        /// </summary>
        /// <returns>
        /// The <see cref="Markdown"/>.
        /// </returns>
        private static Markdown GetMarkdownFormatter()
        {
            var formatter = new Markdown()
                    {
                        ExtraMode = true,
                        SafeMode = false
                    };

            formatter.PrepareLink += PrepareLink;

            return formatter;
        }

        /// <summary>
        /// Handles the PrepareLink event
        /// </summary>
        /// <param name="htmlTag">
        /// The html tag.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool PrepareLink(HtmlTag htmlTag)
        {
            var basePath = PathHelper.GetRootRoute();
            var href = htmlTag.attributes["href"];
            href = string.Format("{0}{1}", basePath.EnsureNotEndsWith('/'), href);
            htmlTag.attributes["href"] = href;

            return true;
        }
    }
}