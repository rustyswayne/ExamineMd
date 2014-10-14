namespace ExamineMd
{
    using System.Text.RegularExpressions;

    using Umbraco.Core;

    /// <summary>
    /// A search helper class.
    /// </summary>
    internal static class SearchHelper
    {
        /// <summary>
        /// Gets a IMdFile key.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GetFileKey(string path, string fileName)
        {
            path = PathHelper.ValidateDocumentPath(path).EnsureForwardSlashes();
            if (path.EndsWith("/")) path = path.Remove(path.LastIndexOf('/'), 1);

            return string.Format("{0}{1}", path.ToLowerInvariant(), fileName.ToLowerInvariant()).ToMd5();
        }

        /// <summary>
        /// Gets a IMdPath key.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string GetPathKey(string path)
        {
            return PathHelper.ValidateSearchablePath(path).ToLowerInvariant().ToMd5();
        }

        /// <summary>
        /// The remove special characters.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string RemoveSpecialCharacters(string input)
        {
            var regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return regex.Replace(input, string.Empty);
        }

    }
}