namespace ExamineMd.Search
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Examine;
    using Examine.LuceneEngine.SearchCriteria;
    using Examine.Providers;
    using Examine.SearchCriteria;

    using ExamineMd.Models;

    /// <summary>
    /// A search helper class.
    /// </summary>
    internal static class SearchHelper
    {
        /// <summary>
        /// Validates and or reformats a path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ValidatePath(string path)
        {
            path = path.Replace("/", "\\");
            path = path.StartsWith("~") ? path.Remove(0, 1) : path;
            return path.StartsWith("\\") ? path.Remove(0, 1) : path;
        }

        /// <summary>
        /// Validates a searchable path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string ValidateSearchablePath(string path)
        {
            return RemoveSpecialCharacters(ValidatePath(path));
        }

        /// <summary>
        /// Builds search criteria for a particular provider
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="providerName">
        /// The provider name.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="ISearchCriteria"/>.
        /// </returns>
        internal static ISearchCriteria BuildCriteria(string searchTerm, string providerName, string[] fields)
        {
            return BuildCriteria(searchTerm, ExamineManager.Instance.SearchProviderCollection[providerName], fields);
        }

        /// <summary>
        /// Builds search criteria for a particular provider.
        /// </summary>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="ISearchCriteria"/>.
        /// </returns>
        internal static ISearchCriteria BuildCriteria(string searchTerm, BaseSearchProvider provider, string[] fields)
        {
            var criteria = provider.CreateSearchCriteria(BooleanOperation.Or);

            return BuildQuery(searchTerm, criteria, fields);
        }

        /// <summary>
        /// Assists in building search criteria
        /// </summary>
        /// <param name="searchString">The search term</param>
        /// <param name="criteria">ISearchCriteria</param>
        /// <param name="textFields">Fields in the index to search</param>
        /// <returns>The <see cref="ISearchCriteria"/></returns>    
        /// <remarks>// our.umbraco.org/forum/developers/extending-umbraco/19329-Search-multiple-fields-for-multiple-terms-with-examine?p=0</remarks>
        internal static ISearchCriteria BuildQuery(string searchString, ISearchCriteria criteria, string[] textFields)
        {
            var terms = searchString.ToSearchTerms();

            foreach (var t in terms.Where(t => !string.IsNullOrEmpty(t.Term)))
            {
                switch (t.SearchTermType)
                {
                    case SearchTermType.SingleWord:
                        criteria.GroupedOr(
                            textFields,
                            new[] { t.Term });
                        break;
                    case SearchTermType.MultiWord:
                        criteria.GroupedOr(
                            textFields,
                            new[] { t.Term.MultipleCharacterWildcard() });
                        break;
                }
            }


            return criteria;
        }

        /// <summary>
        /// BaseSearchProvider extension to build search criteria
        /// </summary>
        /// <param name="provider">
        /// The provider.
        /// </param>
        /// <param name="searchTerm">
        /// The search term.
        /// </param>
        /// <param name="fields">
        /// The fields.
        /// </param>
        /// <returns>
        /// The <see cref="ISearchCriteria"/>.
        /// </returns>
        internal static ISearchCriteria BuildCriteria(this BaseSearchProvider provider, string searchTerm, string[] fields)
        {
            var criteria = provider.CreateSearchCriteria(BooleanOperation.Or);

            return BuildQuery(searchTerm, criteria, fields);
        }

        /// <summary>
        /// Breaks up a search string into multiple search terms.
        /// </summary>
        /// <param name="searchString">
        /// The search string.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{SearchTerm}"/>.
        /// </returns>
        internal static IEnumerable<SearchTerm> ToSearchTerms(this string searchString)
        {
            var terms = new List<SearchTerm>();

            if (searchString.Contains(@"""") && (searchString.Count(t => t == '"') % 2 == 0)) // even number of quotes, more than zero
            {
                // look for any content between quotes
                var quoteRegex = new Regex(@""".+?""");

                foreach (Match item in quoteRegex.Matches(searchString))
                {
                    terms.Add(new SearchTerm() { Term = item.Value.Replace('"', ' ').Trim(), SearchTermType = SearchTermType.MultiWord });

                    // remove them from search string for subsequent parsing
                    searchString = Regex.Replace(searchString, item.Value, String.Empty);
                }
            }

            var singleTerms = searchString.Split(' ').ToList();

            singleTerms.ForEach(t => terms.Add(new SearchTerm() { Term = t, SearchTermType = SearchTermType.SingleWord }));

            return terms;
        }

        /// <summary>
        /// The remove special characters.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        internal static string RemoveSpecialCharacters(string input)
        {
            var regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return regex.Replace(input, string.Empty);
        }

    }
}