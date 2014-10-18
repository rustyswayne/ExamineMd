namespace ExamineMd.Search
{
    using Examine.Providers;

    /// <summary>
    /// A base class for ExamineMd Examine query objects.
    /// </summary>
    public abstract class MdQueryBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdQueryBase"/> class.
        /// </summary>
        /// <param name="searchProvider">
        /// The search provider.
        /// </param>
        protected MdQueryBase(BaseSearchProvider searchProvider)
        {
            Mandate.ParameterNotNull(searchProvider, "searchProvider");
            Searcher = searchProvider;
        }

        /// <summary>
        /// Gets the <see cref="BaseSearchProvider"/>.
        /// </summary>
        protected BaseSearchProvider Searcher { get; private set; }
    }
}