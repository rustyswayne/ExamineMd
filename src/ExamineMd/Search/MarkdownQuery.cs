namespace ExamineMd.Search
{
    using System;

    using Examine;

    /// <summary>
    /// The markdown query.
    /// </summary>
    public class MarkdownQuery : IMarkdownQuery
    {
        /// <summary>
        /// The <see cref="IMdFileQuery"/>.
        /// </summary>
        private Lazy<IMdFileQuery> _fileQuery;

        /// <summary>
        /// The <see cref="IMdPathQuery"/>.
        /// </summary>
        private Lazy<IMdPathQuery> _pathQuery; 
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownQuery"/> class.
        /// </summary>
        public MarkdownQuery()
        {
            Initialize();
        }

        /// <summary>
        /// Gets the <see cref="IMdFileQuery"/>.
        /// </summary>
        public IMdFileQuery Files 
        {
            get
            {
                return _fileQuery.Value;
            } 
        }

        /// <summary>
        /// Gets the <see cref="IMdPathQuery"/>.
        /// </summary>
        public IMdPathQuery Paths
        {
            get
            {
                return _pathQuery.Value;
            }
        }

        /// <summary>
        /// Initializes the query class.
        /// </summary>
        private void Initialize()
        {
            var searcher = ExamineManager.Instance.SearchProviderCollection["ExamineMdSearcher"];

            if (_fileQuery == null)
                _fileQuery = new Lazy<IMdFileQuery>(() => new MdFileQuery(searcher));

            if (_pathQuery == null)
                _pathQuery = new Lazy<IMdPathQuery>(() => new MdPathQuery(searcher));
        }
    }
}