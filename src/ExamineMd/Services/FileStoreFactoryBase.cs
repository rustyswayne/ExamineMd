namespace ExamineMd.Services
{
    /// <summary>
    /// A base class for file store related factories
    /// </summary>
    internal abstract class FileStoreFactoryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileStoreFactoryBase"/> class.
        /// </summary>
        /// <param name="pathToRoot">
        /// The path to root.
        /// </param>
        protected FileStoreFactoryBase(string pathToRoot)
        {
            Mandate.ParameterNotNullOrEmpty(pathToRoot, "pathToRoot");

            this.PathToRoot = pathToRoot;
        }

        /// <summary>
        /// Gets the path to root.
        /// </summary>
        protected string PathToRoot { get; private set; }
    }
}