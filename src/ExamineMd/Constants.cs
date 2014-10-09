namespace ExamineMd
{
    /// <summary>
    /// ExamineMd package constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets the package name.
        /// </summary>
        public static string PackageName
        {
            get
            {
                return "ExamineMd";
            }
        }

        /// <summary>
        /// Gets the md root directory alias.
        /// </summary>
        public static string MdRootDirectoryAlias
        {
            get
            {
                return "ExamineMd:MdRootDirectory";
            }
        }

        /// <summary>
        /// Defines the Examine Index Type.
        /// </summary>
        public static class IndexTypes
        {
            /// <summary>
            /// Gets the Examine IndexType for an ExamineMd document.
            /// </summary>
            public static string ExamineMdDocument
            {
                get
                {
                    return "ExamineMdDocument";
                }
            }
        }
    }
}