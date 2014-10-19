namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Examine.LuceneEngine.SearchCriteria;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;

    /// <summary>
    /// Represents a MdFileQuery.
    /// </summary>
    internal class MdFileQuery : MdQueryBase, IMdFileQuery
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdFileQuery"/> class.
        /// </summary>
        /// <param name="searchProvider">
        /// The search provider.
        /// </param>
        public MdFileQuery(BaseSearchProvider searchProvider)
            : base(searchProvider)
        {
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
            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("key", key);
            var result = Searcher.Search(criteria).FirstOrDefault();
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
            return this.Get(key);
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

            return Searcher.Search(criteria).FirstOrDefault().ToMdFile();
        }

        /// <summary>
        /// Gets a collection of <see cref="IMdFile"/> based on the MD5 path key.
        /// </summary>
        /// <param name="pathKey">
        /// The MD5 path key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IMdFile"/>.
        /// </returns>
        public IEnumerable<IMdFile> GetByPathKey(string pathKey)
        {
            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("pathKey", pathKey)
                    .And()
                    .Field("__IndexType", Constants.IndexTypes.ExamineMdDocument.ToLowerInvariant());

            return Searcher.Search(criteria).Select(x => x.ToMdFile());
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

            return Searcher.Search(criteria).Select(x => x.ToMdFile());
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

            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("pathSearchable", searchPath)
                .And()
                .GroupedOr(new[] { "title", "searchableBody" }, new[] { term, term })
                .Compile();

            return Searcher.Search(criteria).Select(x => x.ToMdFile());
        }

        /// <summary>
        /// List files associated with the path provided
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="includeChildPaths">
        /// Optional parameter to list all documents with paths matching
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> List(string path, bool includeChildPaths = false)
        {
            var searchPath = PathHelper.ValidateSearchablePath(path);

            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);

            if (includeChildPaths)
            {
                if (searchPath.Equals("root", StringComparison.InvariantCultureIgnoreCase))
                {
                    criteria.Field("allDocs", "1")
                        .And()
                        .Field("__IndexType", Constants.IndexTypes.ExamineMdDocument.ToLowerInvariant());
                }
                else
                {
                    criteria.Field("pathSearchable", searchPath)
                        .And()
                        .Field("__IndexType", Constants.IndexTypes.ExamineMdDocument.ToLowerInvariant());
                }
            }
            else
            {
                criteria.Field("pathKey", SearchHelper.GetPathKey(path))
                    .And()
                    .Field("__IndexType", Constants.IndexTypes.ExamineMdDocument.ToLowerInvariant());
            }

            return Searcher.Search(criteria).Select(x => x.ToMdFile());
        }

        /// <summary>
        /// Gets a list of all MarkdownAsHtmlString documents.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> GetAll()
        {
            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);
            criteria.Field("allDocs", "1").And().Field("__IndexType", Constants.IndexTypes.ExamineMdDocument.ToLowerInvariant());

            return Searcher.Search(criteria).Select(x => x.ToMdFile());
        }


        /// <summary>
        /// The build a search criteria.
        /// </summary>
        /// <returns>
        /// The <see cref="ISearchCriteria"/>.
        /// </returns>
        private ISearchCriteria GetBaseSearchCriteria()
        {
            return Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument, BooleanOperation.Or);
        }
    }
}