namespace ExamineMd.DataService
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    using Examine;
    using Examine.LuceneEngine;

    using ExamineMd.Models;
    using ExamineMd.Search;
    using ExamineMd.Services;

    using Newtonsoft.Json;

    using StringExtensions = Umbraco.Core.StringExtensions;

    /// <summary>
    /// The Examine IndexDataService used to index MarkdownAsHtmlString file contents.
    /// </summary>
    public class MarkdownFileIndexDataService : ISimpleDataService 
    {
        /// <summary>
        /// The repository.
        /// </summary>
        private readonly IMarkdownFileService repository = new MarkdownFileService();

        /// <summary>
        /// Returns a collection of all MarkdownAsHtmlString content data.
        /// </summary>
        /// <param name="indexType">
        /// The index type.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{SimpleDataSet}"/>.
        /// </returns>
        public IEnumerable<SimpleDataSet> GetAllData(string indexType)
        {
            
            var dataset = new List<SimpleDataSet>();

            var directories = repository.GetDirectories();

            if (indexType.Equals(Constants.IndexTypes.ExamineMdDocument))
            {
                var count = directories.Count();
 
                // Index the MdFiles
                var all = repository.GetAllMarkdownFiles();
                foreach (var md in all)
                {
                    count++;
                    dataset.Add(BuildMdFileDataSet(md, count));
                }    
            }
            else if (indexType.Equals(Constants.IndexTypes.ExamineMdDirectory))
            {
                var count = 0;
                foreach (var dir in directories)
                {
                    count++;
                    dataset.Add(BuildMdDirectoryDataSet(dir, count));
                }    
            }
            
            return dataset;
        }

        /// <summary>
        /// Builds a MdDirectory data set.
        /// </summary>
        /// <param name="directory">
        /// The directory.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="SimpleDataSet"/>.
        /// </returns>
        private static SimpleDataSet BuildMdDirectoryDataSet(IMdDirectory directory, int id)
        {
            
            return new SimpleDataSet()
                {
                    NodeDefinition = 
                        new IndexedNode()
                            {
                                NodeId = id,
                                Type = Constants.IndexTypes.ExamineMdDirectory
                            },
                    RowData = new Dictionary<string, string>()
                            {
                                { "key", directory.Path.Key },
                                { "path", directory.Path.Value.EnsureForwardSlashes() },
                                { "pathSearchable", PathHelper.ValidateSearchablePath(directory.Path.Value) },
                                { "pathKey", directory.Path.ParentPath().Key },
                                { "allDocs", "1" }
                            }
                };
        }

        /// <summary>
        /// Builds a MdFile data set.
        /// </summary>
        /// <param name="md">
        /// The md.
        /// </param>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="SimpleDataSet"/>.
        /// </returns>
        private static SimpleDataSet BuildMdFileDataSet(IMdFile md, int id)
        {
            return
                new SimpleDataSet()
                {
                    NodeDefinition =
                        new IndexedNode()
                        {
                            NodeId = id,
                            Type = Constants.IndexTypes.ExamineMdDocument
                        },
                    RowData = new Dictionary<string, string>()
                            {
                                { "key", md.Key },
                                { "fileName", md.Path.FileName },
                                { "title", md.Title },
                                { "body", md.Body },
                                { "searchableBody", SearchHelper.RemoveSpecialCharacters(md.Body) },
                                { "path", string.IsNullOrEmpty(md.Path.Value) ? "root" : md.Path.Value.EnsureForwardSlashes() },
                                { "pathSearchable", PathHelper.ValidateSearchablePath(md.Path.Value) + " " + md.Path.FileName.EnsureNotEndsWith(".md") },
                                { "pathKey", md.Path.Key },
                                { "searchableUrl",  PathHelper.GetSearchableUrl(md.Path.Value, md.Path.FileName) },
                                { "metaData", JsonConvert.SerializeObject(md.MetaData) },
                                { "allDocs", "1" },
                                { "createDate", md.DateCreated.ToString("yyyy-MM-dd-HH:mm:ss") }
                            }
                };
            }
    }
}