namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Examine;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;

    /// <summary>
    /// The markdown query.
    /// </summary>
    public class MarkdownQuery : IMarkdownQuery
    {
        /// <summary>
        /// The Examine Search provider.
        /// </summary>
        private readonly BaseSearchProvider _searchProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownQuery"/> class.
        /// </summary>
        public MarkdownQuery()
        {
            _searchProvider = ExamineManager.Instance.SearchProviderCollection["ExamineMdSearcher"];
        }

        /// <summary>
        /// Gets a Markdown document by it's path and file name
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/> or null.
        /// </returns>
        public IMdFile Get(string path, string fileName)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMdFile> Search(string term)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMdFile> Search(string term, string path)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IMdFile> List(string path, bool includeChildPaths = false)
        {
            var searchPath = SearchHelper.ValidateSearchablePath(path);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a list of all Markdown documents.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> GetAll()
        {
            var criteria = this.GetBaseSearchCriteria();
            criteria.Field("allDocs", "1");

            return _searchProvider.Search(criteria).Select(x => x.ToMdFile());
        }

        /// <summary>
        /// The build a search criteria.
        /// </summary>
        /// <returns>
        /// The <see cref="ISearchCriteria"/>.
        /// </returns>
        private ISearchCriteria GetBaseSearchCriteria()
        {
            return _searchProvider.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument, BooleanOperation.Or);
        }

    }
}