namespace ExamineMd
{
    using System.Linq;

    using ExamineMd.Models;

    /// <summary>
    /// Extension methods for <see cref="IMdFile"/>.
    /// </summary>
    public static class MdFileExtensions
    {
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
            return PathHelper.GetSearchableUrl(md.Path.Value, md.FileName);
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