namespace ExamineMd
{
    /// <summary>
    /// Assist in internal path mapping.
    /// </summary>
    public class PathHelper
    {
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
    }
}