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
            var routePath = Constants.MarkdownDocumentRoute;

            if (path.StartsWith(routePath)) path = path.Remove(0, routePath.Length);

            path = path.EnsureBackSlashes();

            return path.StartsWith("~") ? path.Remove(0, 1) : path;
        }

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
        /// Gets the reference path for the file store.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetFileStoreReferencePath(this string path)
        {
            return string.IsNullOrEmpty(path) ? "/" : ValidateDocumentPath(path.RelativePathFromFileStoreRoot());
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
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureForwardSlashes(this string path)
        {
            return path.Replace("\\", "/");
        }

        /// <summary>
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureBackSlashes(this string path)
        {
            return path.Replace("/", "\\");
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
        /// Replaces the indexed value "root" with / for special case root level documents.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string ReplaceRootSlash(this string path)
        {
            return path.Equals("root") ? "/" : path;
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

            return string.IsNullOrEmpty(searchable) ? "root" : searchable;
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
            return url.Replace(Constants.MarkdownDocumentRoute, string.Empty).EnsureForwardSlashes();
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
            if (string.IsNullOrEmpty(fileName)) return string.Empty;

            return string.Format("{0}{1}", ValidateDocumentPath(path).EnsureForwardSlashes().EnsureEndsWith('/'), fileName.Substring(0, fileName.Length - 3).SafeEncodeUrlSegments());
        }

        /// <summary>
        /// Gets the searchable Url.
        /// </summary>
        /// <param name="md">
        /// The md.
        /// </param>
        /// <returns>
        /// The searchable Url.
        /// </returns>
        internal static string SearchableUrl(this IMdFile md)
        {
            return GetSearchableUrl(md.Path.Value, md.FileName);
        }

        /// <summary>
        /// Gets the relative path from the file store root 
        /// </summary>
        /// <param name="path">
        /// The full path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string RelativePathFromFileStoreRoot(this string path)
        {
            var rootPath = IOHelper.MapPath(ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias]);
            rootPath = rootPath.Substring(0, rootPath.Length - 1);
            return path.Replace(rootPath, string.Empty);
        }

        /// <summary>
        /// Gets the virtual content level.
        /// </summary>
        /// <param name="md">
        /// The md.
        /// </param>
        /// <param name="contentLevel">
        /// The content level.
        /// </param>
        /// <returns>
        /// The virtual level.
        /// </returns>
        internal static int GetContentLevel(this IMdFile md, int contentLevel)
        {
            var folders = md.Path.Value.EnsureNotStartsWith('/').EnsureForwardSlashes().Split('/');

            return contentLevel + folders.Count();
        }
    }
}