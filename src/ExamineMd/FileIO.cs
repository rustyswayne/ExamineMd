namespace ExamineMd
{
    using System;
    using System.Configuration;
    using System.IO;

    using Umbraco.Core.IO;

    /// <summary>
    /// The file io helper.
    /// </summary>
    internal static class FileIoHelper
    {
        /// <summary>
        /// Creates path of App_Data\MD\
        /// </summary>
        /// <returns></returns>
        public static string GetMdDir()
        {
            return IOHelper.MapPath(ConfigurationManager.AppSettings[Constants.MdRootDirectoryAlias]);
        }

        /// <summary>
        /// Returns File name from non-Umbraco Class Library's App_Data\MD\ directory
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string GetMdFile(string filename)
        {
             return Path.Combine(GetMdDir(), filename);
        }

        /// <summary>
        /// Determines if MD file extists, assuming full path to MD is provided
        /// </summary>
        /// <param name="filenameWithPath"></param>
        /// <returns></returns>
        public static bool FileExists(string filenameWithPath)
        {
            return File.Exists(filenameWithPath);
        }

        public static string GetMdFileContents(string filename)
        {
            var filewithpath = GetMdFile(filename);

            if (FileExists(filewithpath))
            {
                return File.ReadAllText(filewithpath);
            }

            return null;
        }
    }
}
