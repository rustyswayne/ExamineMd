namespace ExamineMd.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Examine;
    using Examine.LuceneEngine;

    using ExamineMd.Persistence.Repositories;

    using umbraco.cms.businesslogic.packager;

    /// <summary>
    /// The Examine IndexDataService used to index Markdown file contents.
    /// </summary>
    public class MarkdownFileIndexDataService : ISimpleDataService 
    {
        /// <summary>
        /// Returns a collection of all Markdown content data.
        /// </summary>
        /// <param name="indexType">
        /// The index type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{SimpleDataSet}"/>.
        /// </returns>
        public IEnumerable<SimpleDataSet> GetAllData(string indexType)
        {
            var repository = new MarkdownFileRepository();

            var all = repository.GetAllMarkdownFiles();

            var count = 0;

            var dataset = new List<SimpleDataSet>();

            foreach (var md in all)
            {
                count++;
                var set =
                    new SimpleDataSet()
                        {
                            NodeDefinition =
                                new IndexedNode()
                                    {
                                        NodeId = count,
                                        Type = Constants.IndexTypes.ExamineMdDocument
                                    },
                            RowData = new Dictionary<string, string>()
                                {
                                    { "fileName", RemoveSpecialCharacters(md.FileName) },
                                    { "title", RemoveSpecialCharacters(md.Title) },
                                    { "body", RemoveSpecialCharacters(md.Body) },
                                    { "path", RemoveSpecialCharacters(md.Path) },
                                    { "createDate", md.DateCreated.ToString("yyyy-MM-dd-HH:mm:ss") }
                                }
                        };
                dataset.Add(set);
            }

            return dataset;
        }

        private static string RemoveSpecialCharacters(string input)
        {
            var regex = new Regex("(?:[^a-z0-9 ]|(?<=['\"])s)", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return regex.Replace(input, string.Empty);
        }
    }
}