namespace ExamineMd.DataService
{
    using System.Collections.Generic;
    using System.Linq;

    using Examine.LuceneEngine;

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
            return Enumerable.Empty<SimpleDataSet>();
        }
    }
}