namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Examine;
    using Examine.LuceneEngine.SearchCriteria;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;

    using Umbraco.Core;

    /// <summary>
    /// A search helper class.
    /// </summary>
    internal static class SearchHelper
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
            path = path.Replace("/", "\\");
            path = path.StartsWith("~") ? path.Remove(0, 1) : path;
            return path.StartsWith("\\") ? path.Remove(0, 1) : path;
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
            return ValidatePath(path).Replace("\\", " ");
        }

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
            return string.Format("{0}{1}", ValidatePath(path), fileName).ToMd5();
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