﻿namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Examine.LuceneEngine.SearchCriteria;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;
    using System.Text.RegularExpressions;

    public class SearchTerm
    {
        public string Term { get; set; }
        public SearchTermType TermType { get; set; }
    }


    public enum SearchTermType
    {
        SingleWord,
        MultiWord
    }

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
        /// Searches for a term across all paths 
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        public IEnumerable<IMdFile> SearchAllRecords(string term)
        {
            var criteria = Searcher.CreateSearchCriteria(Constants.IndexTypes.ExamineMdDocument);

            // DFB: picked most useful terms -- more could be added
            var query = BuildQuery(new string[] { "title", "bodyText", "metaData" }, term, criteria);

            return Searcher.Search(query).Select(x => x.ToMdFile());
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

        /// <summary>
        /// Build Query to handle mutliple column search
        /// https://our.umbraco.org/forum/developers/extending-umbraco/19329-Search-multiple-fields-for-multiple-terms-with-examine
        /// http://stackoverflow.com/questions/468405/how-to-incorporate-multiple-fields-in-queryparser
        /// </summary>
        /// <param name="textFields">list of fields to search</param>
        /// <param name="searchString">search term</param>
        /// <param name="criteria"></param>
        /// <returns></returns>
        private static ISearchCriteria BuildQuery(string[] textFields, string searchString, ISearchCriteria criteria)
        {
            List<SearchTerm> Terms = new List<SearchTerm>();

            if ((searchString.Contains(@"""")) && (searchString.Count(t => t == '"') % 2 == 0)) // even number of quotes, more than zero
            {
                Regex quoteRegex = new Regex(@""".+?"""); // look for any content between quotes
                foreach (Match item in quoteRegex.Matches(searchString))
                {
                    Terms.Add(new SearchTerm() { Term = item.Value.Replace('"', ' ').Trim(), TermType = SearchTermType.MultiWord });
                    searchString = Regex.Replace(searchString, item.Value, string.Empty); // remove them from search string for subsequent parsing
                }
            }

            List<string> singleTerms = new List<string>();
            singleTerms = searchString.Split(' ').ToList();
            singleTerms.ForEach(t => Terms.Add(new SearchTerm() { Term = t, TermType = SearchTermType.SingleWord }));

            foreach (SearchTerm t in Terms)
            {
                if (!string.IsNullOrEmpty(t.Term))
                {
                    switch (t.TermType)
                    {
                        case SearchTermType.SingleWord:
                            criteria.GroupedOr(textFields,
                                new IExamineValue[] { Examine.LuceneEngine.SearchCriteria.LuceneSearchExtensions.Fuzzy(t.Term, 0.4F) });
                            break;
                        case SearchTermType.MultiWord:
                            criteria.GroupedOr(textFields,
                                new IExamineValue[] { Examine.LuceneEngine.SearchCriteria.LuceneSearchExtensions.Escape(t.Term) });
                            break;
                        default:
                            break;
                    }
                }
            }

            return criteria;
        }

    }
}