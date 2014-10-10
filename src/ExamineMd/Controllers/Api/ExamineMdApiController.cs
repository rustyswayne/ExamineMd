namespace ExamineMd.Controllers.Api
{
    using System.Collections.Generic;
    using System.Web.Http;

    using ExamineMd.Models;
    using ExamineMd.Search;

    using Umbraco.Web.Mvc;
    using Umbraco.Web.WebApi;

    /// <summary>
    /// The ExamineMdApiController .
    /// </summary>
    [PluginController("ExamineMd")]
    public class ExamineMdApiController : UmbracoApiController
    {
        /// <summary>
        /// The <see cref="IMarkdownQuery"/>
        /// </summary>
        private readonly IMarkdownQuery _query;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamineMdApiController"/> class.
        /// </summary>
        public ExamineMdApiController()
            : this(new MarkdownQuery())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExamineMdApiController"/> class.
        /// </summary>
        /// <param name="query">
        /// The <see cref="IMarkdownQuery"/>
        /// </param>
        internal ExamineMdApiController(IMarkdownQuery query)
        {
            Mandate.ParameterNotNull(query, "query");

            _query = query;
        }

        /// <summary>
        /// Gets a <see cref="IMdFile"/> by it's key
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        [HttpGet]
        public IMdFile Get(string key)
        {
            return _query.Get(key);
        }

        /// <summary>
        /// Gets a <see cref="IMdFile"/> by it's path and file name
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        [HttpGet]
        public IMdFile Get(string path, string fileName)
        {
            return _query.Get(path, fileName);
        }

        /// <summary>
        /// Searches the markdown documents for a specific term.
        /// </summary>
        /// <param name="term">
        /// The term.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        [HttpGet, HttpPost]
        public IEnumerable<IMdFile> Search(string term)
        {
            return _query.Search(term);
        }

        /// <summary>
        /// Searches the markdown documents for a specific term filtered on a path.
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
        [HttpGet, HttpPost]
        public IEnumerable<IMdFile> Search(string term, string path)
        {
            return _query.Search(term, path);
        }

        /// <summary>
        /// The list.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        [HttpGet, HttpPost]
        public IEnumerable<IMdFile> List(string path)
        {
            return _query.List(path);
        }

        /// <summary>
        /// Returns the collection of all Markdown files.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable{IMdFile}"/>.
        /// </returns>
        [HttpGet]
        public IEnumerable<IMdFile> GetAll()
        {
            return _query.GetAll();
        }
    }
}