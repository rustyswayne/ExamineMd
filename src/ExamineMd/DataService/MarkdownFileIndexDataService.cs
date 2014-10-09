namespace ExamineMd.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Examine;
    using Examine.LuceneEngine;

    using ExamineMd.Search;
    using ExamineMd.Services;

    using Newtonsoft.Json;

    using umbraco.cms.businesslogic.packager;

    /// <summary>
    /// The Examine IndexDataService used to index Markdown file contents.
    /// </summary>
    public class MarkdownFileIndexDataService : ISimpleDataService 
    {
        private static readonly object Locker = new object();

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
            var repository = new MarkdownFileService();

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
                                    { "fileName", md.FileName },
                                    { "title", md.Title },
                                    { "body", md.Body },
                                    { "searchableBody", SearchHelper.RemoveSpecialCharacters(md.Body) },
                                    { "path", md.Path },
                                    { "pathSearchable", SearchHelper.RemoveSpecialCharacters(md.Path) },
                                    { "metaData", JsonConvert.SerializeObject(md.MetaData) },
                                    { "allDocs", "1" },
                                    { "createDate", md.DateCreated.ToString("yyyy-MM-dd-HH:mm:ss") }
                                }
                        };
                dataset.Add(set);
            }

            return dataset;
        }
    }
}