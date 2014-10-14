namespace ExamineMd
{
    using System;
    using System.Configuration;
    using System.IO;

    using ExamineMd.Models;

    using Umbraco.Core.IO;

    /// <summary>
    /// Path related extensions.
    /// </summary>
    public static class PathingExtensions
    {
        /// <summary>
        /// Returns true if the path represents the "root" virtual path
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsRootPath(this IMdPath path)
        {
            return path.Value.Equals("\\", StringComparison.Ordinal);
        }

        /// <summary>
        /// Returns the parent path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IMdPath"/>.
        /// </returns>
        public static IMdPath ParentPath(this IMdPath path)
        {
            if (path.IsRootPath()) return path;

            var lastIndex = path.Value.LastIndexOf("\\", StringComparison.Ordinal);

            var pathString = lastIndex <= 0 ? "\\" : path.Value.Substring(0, path.Value.LastIndexOf("\\", StringComparison.Ordinal));

            return pathString.Equals("\\", StringComparison.Ordinal) 
                ? new MdPath(pathString) : 
                new MdPath(pathString.EnsureNotEndsWith('\\'));
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
        internal static string GetFileStoreReferencePath(this string path)
        {
            return string.IsNullOrEmpty(path) ? "/" : PathHelper.ValidateDocumentPath(path.RelativePathFromFileStoreRoot());
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