namespace ExamineMd
{
    using System.Configuration;

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
        /// Gets the markdown document route.
        /// </summary>
        public static string MarkdownDocumentRoute
        {
            get
            {
                return ConfigurationManager.AppSettings["ExamineMd:DocumentRoute"].ToLowerInvariant();
            }
        }

        /// <summary>
        /// Gets the markdown listing route.
        /// </summary>
        public static string MarkdownListingRoute
        {
            get
            {
                return ConfigurationManager.AppSettings["ExamineMd:ListingRoute"].ToLowerInvariant();
            }
        }

        /// <summary>
        /// The content types.
        /// </summary>
        public static class ContentTypes
        {
            /// <summary>
            /// Gets the ExamineMd content type alias.
            /// </summary>
            public static string ExamineMd
            {
                get
                {
                    return "ExamineMd";
                }
            }

            /// <summary>
            /// Gets the content type for an ExamineMd document.
            /// </summary>
            public static string ExamineMdMarkdownDocument
            {
                get
                {
                    return "ExamineMdMarkdownDocument";
                }
            }


            /// <summary>
            /// Gets the content type for an ExamineMd listing.
            /// </summary>
            public static string ExamineMdMarkdownListing
            {
                get
                {
                    return "ExamineMdMarkdownListing";
                }
            }
        }
        

        /// <summary>
        /// Gets the path to the App_Pluggins/ExamineMd folder.
        /// </summary>
        public static string AppPluginFolder
        {
            get
            {
                return "~/App_Plugins/ExamineMd/";
            }
        }

        /// <summary>
        /// Defines the Examine Index Type.
        /// </summary>
        public static class IndexTypes
        {
            /// <summary>
            /// Gets the Examine IndexType for an ExamineMd markdown documents.
            /// </summary>
            public static string ExamineMdDocument
            {
                get
                {
                    return "ExamineMdDocument";
                }
            }

            /// <summary>
            /// Gets the Examine IndexType for an ExamineMd document directory.
            /// </summary>
            public static string ExamineMdDirectory
            {
                get
                {
                    return "ExamineMdDirectory";
                }
            }
        }
    }
}