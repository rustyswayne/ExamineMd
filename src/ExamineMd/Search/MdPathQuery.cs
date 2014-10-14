namespace ExamineMd.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Examine.Providers;

    using ExamineMd.Models;

    /// <summary>
    /// Performs <see cref="MdPath"/> queries.
    /// </summary>
    internal class MdPathQuery : MdQueryBase, IMdPathQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdPathQuery"/> class.
        /// </summary>
        /// <param name="searchProvider">
        /// The search provider.
        /// </param>
        public MdPathQuery(BaseSearchProvider searchProvider)
            : base(searchProvider)
        {
        }

        /// <summary>
        /// The MD5 key
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IMdPath"/>.
        /// </returns>
        public IMdPath Get(string key)
        {
            var criteria = this.Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDirectory);
            criteria.Field("key", key);
            var result = Searcher.Search(criteria).FirstOrDefault();
            return result != null ? result.ToMdPath() : null;
        }

        /// <summary>
        /// Gets collection of <see cref="IMdPath"/> (directories) associated with the path key.
        /// </summary>
        /// <param name="pathKey">
        /// The path key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IMdPath"/> (directories) associated with the path key.
        /// </returns>
        public IEnumerable<IMdPath> GetByPathKey(string pathKey)
        {
            var criteria = this.Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDirectory);
            criteria.Field("pathKey", pathKey).And().Field("__IndexType", Constants.IndexTypes.ExamineMdDirectory.ToLowerInvariant());
            return Searcher.Search(criteria).Select(x => x.ToMdPath());
        }

        /// <summary>
        /// Gets a collection of all paths.
        /// </summary>
        /// <returns>
        /// A collection of all paths.
        /// </returns>
        public IEnumerable<IMdPath> GetAll()
        {
            var criteria = this.Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDirectory);
            criteria.Field("allDocs", "1").And().Field("__IndexType", Constants.IndexTypes.ExamineMdDirectory.ToLowerInvariant());
            return this.Searcher.Search(criteria).Select(x => x.ToMdPath());

        }
    }
}