namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Examine;
    using Examine.LuceneEngine.SearchCriteria;
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
        /// Gets a file by it's key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        public IMdFile Get(string key)
        {
            var criteria = _searchProvider.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("key", key);
            var result = _searchProvider.Search(criteria).FirstOrDefault();
            return result != null ? result.ToMdFile() : null;
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
            var key = SearchHelper.GetFileKey(path, fileName);
            return Get(key);
        }

        /// <summary>
        /// Searches for a term
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> Search(string term)
        {
            var criteria = this.GetBaseSearchCriteria();
            criteria.Field("title", term.Boost(1.5f)).Or().Field("searchableBody", term);
            
            return _searchProvider.Search(criteria).Select(x => x.ToMdFile());
        }

        /// <summary>
        /// Searches for a term limited by a path
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> Search(string term, string path)
        {
            var searchPath = SearchHelper.ValidateSearchablePath(path);

            var criteria = _searchProvider.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("pathSearchable", searchPath)
                .And()
                .GroupedOr(new[] { "title", "searchableBody" }, new[] { term, term })
                .Compile();

            return _searchProvider.Search(criteria).Select(x => x.ToMdFile());
        }

        /// <summary>
        /// List files associated with the path provided
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> List(string path)
        {
            var searchPath = SearchHelper.ValidateSearchablePath(path);

            var criteria = _searchProvider.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("pathSearchable", searchPath);

            return _searchProvider.Search(criteria).Select(x => x.ToMdFile());
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