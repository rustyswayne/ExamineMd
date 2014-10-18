namespace ExamineMd
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Web;

    using Lucene.Net.Search;

    using Umbraco.Core;

    /// <summary>
    /// String extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// The <see cref="System.Globalization.TextInfo"/>.
        /// </summary>
        private static readonly TextInfo TextInfo = new CultureInfo("en", false).TextInfo;

        /// <summary>
        /// The get title from file name.
        /// </summary>
        /// <param name="value">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string AsFormattedTitle(this string value)
        {
            var name = value.EndsWith(".md", StringComparison.InvariantCultureIgnoreCase)
                           ? value.Remove(value.Length - 3, 3)
                           : value;

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
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="value">
        /// The value to replace backslashes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureForwardSlashes(this string value)
        {
            return value.Replace("\\", "/");
        }

        /// <summary>
        /// Replaces \ with / in a path.
        /// </summary>
        /// <param name="value">
        /// The value to replace forwardslashes.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureBackSlashes(this string value)
        {
            return value.Replace("/", "\\");
        }

        /// <summary>
        /// Ensures a string both starts and ends with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureStartsAndEndsWith(this string input, char value)
        {
            return input.EnsureStartsWith(value).EnsureEndsWith(value);
        }

        /// <summary>
        /// Ensures a string does not end with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotEndsWith(this string input, char value)
        {
            return !input.EndsWith(value.ToString(CultureInfo.InvariantCulture)) ? input : 
                input.Remove(input.LastIndexOf(value), 1);
        }

        /// <summary>
        /// Ensures a string does not end with a string.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string EnsureNotEndsWith(this string input, string value)
        {
            return !input.EndsWith(value) ? input : input.Remove(input.LastIndexOf(value, StringComparison.OrdinalIgnoreCase), value.Length);
        }

        /// <summary>
        /// Ensures a string does not start with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotStartsWith(this string input, char value)
        {
            return !input.StartsWith(value.ToString(CultureInfo.InstalledUICulture)) ? input : input.Remove(0, 1);
        }

        /// <summary>
        /// Ensures a string does not start or end with a character.
        /// </summary>
        /// <param name="input">
        /// The input string.
        /// </param>
        /// <param name="value">
        /// The char value to assert
        /// </param>
        /// <returns>
        /// The asserted string.
        /// </returns>
        public static string EnsureNotStartsOrEndsWith(this string input, char value)
        {
            return input.EnsureNotStartsWith(value).EnsureNotEndsWith(value);
        }
    }
}