namespace ExamineMd
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;

    using ClientDependency.Core.Mvc;

    using Umbraco.Core;

    /// <summary>
    /// Extension methods to assist in design and layout.
    /// </summary>
    public static class DesignTimeExtensions
    {

        /// <summary>
        /// Adds ExamineMd Asset CSS file to ClientDependency.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlHelper"/>.
        /// </returns>
        public static HtmlHelper RequiresPackageCss(this HtmlHelper html, string fileName)
        {
            return html.RequiresCss(string.Format("{0}{1}", PathHelper.GetAssetsPath(), fileName));
        }

        /// <summary>
        /// Adds ExamineMd Asset JS file to ClientDependency.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="HtmlHelper"/>.
        /// </returns>
        public static HtmlHelper RequiresPackageJs(this HtmlHelper html, string fileName)
        {
            return html.RequiresJs(string.Format("{0}{1}", PathHelper.GetAssetsPath(), fileName));
        }

        /// <summary>
        /// Gets an ExamineMd partial view.
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <param name="viewData">
        /// The view Data.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString ExamineMdPartial(this HtmlHelper html, string fileName, ViewDataDictionary viewData = null)
        {
            return html.Partial(PathHelper.GetViewPath(string.Format("partials/{0}", fileName)), viewData);
        }
    }
}