namespace ExamineMd
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web;

    using Umbraco.Core;

    public static class StringExtensions
    {
        /// <summary>
        /// The <see cref="System.Globalization.TextInfo"/>.
        /// </summary>
        private static readonly TextInfo TextInfo = new CultureInfo("en", false).TextInfo;


        /// <summary>
        /// The get title from file name.
        /// </summary>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetTitleFromFileName(this string fileName)
        {
            var name = fileName.EndsWith(".md", StringComparison.InvariantCultureIgnoreCase)
                           ? fileName.Remove(fileName.Length - 3, 3)
                           : fileName;

            name = name.Replace("-", " ").Replace("_", " ");

            return name.ToTitleCase();
        }


        /// <summary>
        /// The to title case.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToTitleCase(this string value)
        {
            return TextInfo.ToTitleCase(value);
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
    }
}