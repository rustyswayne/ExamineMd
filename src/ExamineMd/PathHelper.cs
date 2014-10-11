namespace ExamineMd
{
    using System.Configuration;

    using ExamineMd.Models;

    using Umbraco.Core;
    using Umbraco.Core.IO;

    /// <summary>
    /// Assist in internal path mapping.
    /// </summary>
    public static class PathHelper
    {
        /// <summary>
        /// Validates and or reformats a path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ValidatePath(string path)
        {
            var routePath = GetRootRoute();

            if (path.StartsWith(routePath)) path = path.Remove(0, routePath.Length);

            path = path.UseBackSlashes();

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
        /// Gets the route to the Route.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetRootRoute()
        {
            return ConfigurationManager.AppSettings[Constants.MdDefaultRoute].Replace("~", string.Empty).ToLowerInvariant();
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
            return string.IsNullOrEmpty(path) ? "/" : ValidatePath(path.RelativePathFromFileStoreRoot());
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
        public static string UseForwardSlashes(this string path)
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
        public static string UseBackSlashes(this string path)
        {
            return path.Replace("/", "\\");
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
            var searchable = ValidatePath(path).Replace("\\", " ").Replace("/", " ").Trim();

            return string.IsNullOrEmpty(searchable) ? "root" : searchable;
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

            return string.Format("{0}{1}", ValidatePath(path).UseForwardSlashes().EnsureEndsWith('/'), fileName.Substring(0, fileName.Length - 3).SafeEncodeUrlSegments());
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
            return GetSearchableUrl(md.Path, md.FileName);
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
    }
}