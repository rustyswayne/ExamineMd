namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Examine;
    using Examine.LuceneEngine.SearchCriteria;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;
    using ExamineMd.Services;

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
        /// The _file service.
        /// </summary>
        private readonly Lazy<MarkdownFileService> _fileService = new Lazy<MarkdownFileService>(() => new MarkdownFileService());

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
        /// Gets a MarkdownAsHtmlString document by it's path and file name
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
        /// Gets a <see cref="IMdFile"/> by it's Url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        public IMdFile GetByUrl(string url)
        {

            var criteria = this.GetBaseSearchCriteria();
            criteria.Field("searchableUrl", PathHelper.ValidateSearchableUrl(url));

            return _searchProvider.Search(criteria).FirstOrDefault().ToMdFile();
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
            var searchPath = PathHelper.ValidateSearchablePath(path);

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
        /// <param name="includeChildPaths">If true, the search returns all document on child paths as well</param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> List(string path, bool includeChildPaths = false)
        {
            var searchPath = PathHelper.ValidateSearchablePath(path);

            var criteria = includeChildPaths
                ? GetBaseSearchCriteria()
                : _searchProvider.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);

            criteria.Field("pathSearchable", searchPath);

                return _searchProvider.Search(criteria).Select(x => x.ToMdFile());            
        }

        /// <summary>
        /// Gets a list of all MarkdownAsHtmlString documents.
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
        /// Gets a collection of all paths.
        /// </summary>
        /// <returns>
        /// A collection of all paths.
        /// </returns>
        public IEnumerable<string> GetAllPaths()
        {
            return _fileService.Value.GetDirectories().Select(x => x.Path);
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