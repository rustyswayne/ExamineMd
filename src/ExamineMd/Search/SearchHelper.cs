namespace ExamineMd.Search
{
    /// <summary>
    /// A search helper class.
    /// </summary>
    internal class SearchHelper
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
    }
}