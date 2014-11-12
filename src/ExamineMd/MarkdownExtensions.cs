namespace ExamineMd
{
    using System;
    using System.Web;
    using System.Web.Mvc;

    using Controllers.Api;
    using Examine;
    
    using MarkdownDeep;
    using Models;

    using Umbraco.Core.IO;

    /// <summary>
    /// Extension methods for MarkdownAsHtmlString.
    /// </summary>
    public static class MarkdownExtensions
    {

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
        /// Transforms the file body text into Html using MarkdownAsHtmlString Transformation.
        /// </summary>
        /// <param name="document">
        /// The document.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString BodyHtml(this IVirtualMarkdownDocument document)
        {
            var formatter = GetMarkdownFormatter();
            
            formatter.UrlBaseLocation = PathHelper.MakeAbsolutUrl(document.Url, string.Empty);
            formatter.UrlRootLocation = PathHelper.MakeAbsolutUrl(document.RootContent.Url, string.Empty);

            var virtualPath =
                IOHelper.ResolveUrl((
                    PathHelper.GetPhysicalPathToFileStore().EnsureNotEndsWith("\\") + document.Markdown.Path.Value).EnsureForwardSlashes());

            return string.IsNullOrEmpty(document.Markdown.Body) ? MvcHtmlString.Empty : new MvcHtmlString(formatter.Transform(document.Markdown.Body).Replace("__markdown__src__", virtualPath.EnsureStartsAndEndsWith('/')));
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
        internal static IHtmlString BodyHtml(this IMdFile file)
        {
            var formatter = GetMarkdownFormatter();

            return string.IsNullOrEmpty(file.Body) ? MvcHtmlString.Empty : new MvcHtmlString(formatter.Transform(file.Body));
        }

        ///// <summary>
        ///// Converts the body to an Html string
        ///// </summary>
        ///// <param name="file">
        ///// The file.
        ///// </param>
        ///// <returns>
        ///// The <see cref="IMdFile"/>.
        ///// </returns>
        ///// <remarks>
        ///// This is internally used by <see cref="SearchApiController"/> to minimize the response size.
        ///// </remarks>
        //internal static IMdFile ConvertBody(this IMdFile file)
        //{
        //    file.Body = file.BodyHtml().ToHtmlString();
        //    return file;
        //}

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
                        SafeMode = false,
                        NewWindowForExternalLinks = true
                    };

            formatter.PrepareLink += PrepareLink;
            formatter.PrepareImage += PrepareImage;

            return formatter;
        }

        /// <summary>
        /// Handles the PrepareImage.
        /// </summary>
        /// <param name="htmlTag">
        /// The html tag.
        /// </param>
        /// <param name="b">
        /// The b.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool PrepareImage(HtmlTag htmlTag, bool b)
        {
            var src = htmlTag.attributes["src"];
            htmlTag.attributes["src"] = string.Format("__markdown__src__{0}", src); 
            htmlTag.attributes["class"] = "markdown-image";
            
            return true;
        }

        /// <summary>
        /// Handles the PrepareLink
        /// </summary>
        /// <param name="htmlTag">
        /// The html tag.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        private static bool PrepareLink(HtmlTag htmlTag)
        {
            htmlTag.attributes["class"] = "markdown-link";
            if (htmlTag.attributes["href"].EndsWith(".md"))
                htmlTag.attributes["href"] = htmlTag.attributes["href"].Substring(0, htmlTag.attributes["href"].Length - 3);
            return true;
        }
    }
}