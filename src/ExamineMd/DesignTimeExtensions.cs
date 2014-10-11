namespace ExamineMd
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

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
    }
}