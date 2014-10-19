namespace ExamineMd
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Runtime.Remoting.Messaging;
    using System.Web;

    using ExamineMd.Models;

    using Umbraco.Core;
    using Umbraco.Core.IO;

    /// <summary>
    /// Assist in internal path mapping.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Gets the physical path to the markdown file store.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetPhysicalPathToFileStore()
        {
            return ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias];
        }


        /// <summary>
        /// Gets the virtual path to the ExamineMd View folder.
        /// </summary>
        /// <param name="viewName">
        /// Then name of the view
        /// </param>
        /// <returns>
        /// The virtual path to the views folder.
        /// </returns>
        public static string GetViewPath(string viewName)
        {
            return string.Format("{0}Views/{1}.cshtml", Constants.AppPluginFolder, viewName);
        }

        /// <summary>
        /// Gets the virtual path to the ExamineMd Assets folder.
        /// </summary>
        /// <returns>
        /// The virtual path to the assets folder.
        /// </returns>
        public static string GetAssetsPath()
        {
            return string.Format("{0}{1}", Constants.AppPluginFolder, "Assets/");
        }

        /// <summary>
        /// Encodes url segments.
        /// </summary>
        /// <param name="urlPath">
        /// The url path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <remarks>   
        /// https://github.com/Shandem/Articulate/blob/master/Articulate/StringExtensions.cs
        /// </remarks>
        public static string SafeEncodeUrlSegments(this string urlPath)
        {
            return string.Join("/",
                urlPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => HttpUtility.UrlEncode(x).Replace("+", "%20"))
                    .WhereNotNull()
                //we are not supporting dots in our URLs it's just too difficult to
                // support across the board with all the different config options
                    .Select(x => x.Replace('.', '-')));
        }
        
        /// <summary>
        /// Validates and or reformats a document path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ValidateDocumentPath(string path)
        {
            if (string.IsNullOrEmpty(path)) return "\\";

            path = path.EnsureBackSlashes();

            return path.StartsWith("~") ? path.Remove(0, 1) : path;
        }

        /// <summary>
        /// Validates a searchable path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string ValidateSearchablePath(string path)
        {
            var searchable = ValidateDocumentPath(path).Replace("\\", " ").Trim();

            return string.IsNullOrEmpty(searchable) ? "root" : SearchHelper.RemoveSpecialCharacters(searchable);
        }

        /// <summary>
        /// Validates a search Url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The validated search rule.
        /// </returns>
        internal static string ValidateSearchableUrl(string url)
        {
            return url.EnsureForwardSlashes();
        }

        /// <summary>
        /// Gets the searchable Url.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The searchable Url.
        /// </returns>
        internal static string GetSearchableUrl(string path, string fileName)
        {
            return string.IsNullOrEmpty(fileName) ? 
                string.Empty : 
                string.Format("{0}{1}", ValidateDocumentPath(path).EnsureForwardSlashes().EnsureEndsWith('/'), GetFileNameForUrl(fileName));
        }

        /// <summary>
        /// Gets the file name for use in a Url.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GetFileNameForUrl(string fileName)
        {
            return fileName.Substring(0, fileName.Length - 3).SafeEncodeUrlSegments();
        }

        /// <summary>
        /// Gets the absolute absolut url.
        /// </summary>
        /// <param name="relativeUrl">
        /// The relative url.
        /// </param>
        /// <param name="queryString">
        /// The query string.
        /// </param>
        /// <returns>
        /// The absolute url.
        /// </returns>
        /// <exception cref="NullReferenceException">
        /// Throws a null reference exception if the HttpContext is null.
        /// </exception>
        /// <remarks>
        /// https://github.com/rustyswayne/Buzz.Hybrid/blob/master/src/Buzz.Hybrid/HttpContextExtensions.cs
        /// </remarks>
        internal static string MakeAbsolutUrl(string relativeUrl, string queryString)
        {
            if (relativeUrl.StartsWith("http")) return relativeUrl;

            var context = HttpContext.Current;
            if (context == null) throw new NullReferenceException("The HttpContext is null");

            var protocol = context.Request.IsSecureConnection ? "https://" : "http://";
            var host = context.Request.ServerVariables["SERVER_NAME"];
            var port = context.Request.ServerVariables["SERVER_PORT"];
            port = port == "80" ? string.Empty : string.Format(":{0}", port);

            if (!relativeUrl.StartsWith("/")) relativeUrl = string.Format("/{0}", relativeUrl);
            if (!string.IsNullOrEmpty(queryString) && !queryString.StartsWith("?")) queryString = string.Format("?{0}", queryString);

            return string.Format("{0}{1}{2}{3}{4}", protocol, host, port, relativeUrl, queryString);
        }
    }
}