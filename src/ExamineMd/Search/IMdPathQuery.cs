namespace ExamineMd.Search
{
    using System.Collections.Generic;

    using ExamineMd.Models;

    /// <summary>
    /// Defines the MdPathQuery.
    /// </summary>
    public interface IMdPathQuery
    {
        /// <summary>
        /// The MD5 key
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="IMdPath"/>.
        /// </returns>
        IMdPath Get(string key);

        /// <summary>
        /// Gets collection of <see cref="IMdPath"/> (directories) associated with the path key.
        /// </summary>
        /// <param name="pathKey">
        /// The path key.
        /// </param>
        /// <returns>
        /// The collection of <see cref="IMdPath"/> (directories) associated with the path key.
        /// </returns>
        IEnumerable<IMdPath> GetByPathKey(string pathKey);

        /// <summary>
        /// Gets a <see cref="IMdPath"/> by it's Url.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="IMdPath"/>.
        /// </returns>
        IMdPath GetByUrl(string url); 
            
        /// <summary>
        /// Gets a collection of all paths.
        /// </summary>
        /// <returns>
        /// A collection of all paths.
        /// </returns>
        IEnumerable<IMdPath> GetAll(); 
    }
}