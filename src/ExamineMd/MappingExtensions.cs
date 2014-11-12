using System.Linq;

namespace ExamineMd
{
    using System;
    using System.Collections.Generic;

    using Examine;

    using ExamineMd.Models;

    using Newtonsoft.Json;

    /// <summary>
    /// Mapping extensions.
    /// </summary>
    internal static class MappingExtensions
    {
        /// <summary>
        /// Maps an examine <see cref="SearchResult"/> to a <see cref="IMdFile"/>.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="IMdFile"/>.
        /// </returns>
        public static IMdFile ToMdFile(this SearchResult result)
        {
            return new MdFile(result.FieldAsString("key"))
            {
                Path = new MdPath(SearchHelper.GetPathKey(result.FieldAsString("path")), result.FieldAsString("path").ReplaceRootSlash(), result.FieldAsString("fileName")),
                Title = result.FieldAsString("title"),
                Body = result.FieldAsString("body"),
                MetaData = result.FieldAsMetaItemCollection(),
                DateCreated = result.FieldAsDateTime("createDate")
            };
        }

        /// <summary>
        /// Maps an examine <see cref="SearchResult"/> to a <see cref="IMdPath"/>.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <returns>
        /// The <see cref="IMdPath"/>.
        /// </returns>
        public static IMdPath ToMdPath(this SearchResult result)
        {
            return new MdPath(result.FieldAsString("key"), result.FieldAsString("path").EnsureBackSlashes(), result.FieldAsString("fileName"));
        }

        /// <summary>
        /// Returns the field as a string.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string FieldAsString(this SearchResult result, string fieldName)
        {
            return !result.Fields.ContainsKey(fieldName) ? string.Empty : result.Fields[fieldName];
        }

        /// <summary>
        /// Returns the field as date time.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime"/>.
        /// </returns>
        private static DateTime FieldAsDateTime(this SearchResult result, string fieldName)
        {
            DateTime value;    
            return DateTime.TryParse(result.FieldAsString(fieldName), out value) ? value : DateTime.MinValue;
        }

        /// <summary>
        /// Returns the collection of MdMetaItem.
        /// </summary>
        /// <param name="result">
        /// The result.
        /// </param>
        /// <param name="fieldName">
        /// The field name.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{IMdMetaItem}"/>.
        /// </returns>
        private static IMdMetaData FieldAsMetaItemCollection(this SearchResult result, string fieldName = "metaData")
        {
            return !result.Fields.ContainsKey(fieldName)
                       ? new MdMetaData()
                       {
                           MetaDescription = string.Empty,
                           PageTitleLinks = string.Empty,
                           PageTitle = string.Empty,
                           Relevance = string.Empty,
                           Revision = string.Empty,           
                           Items   = Enumerable.Empty<MdDataItem>()
                       }
                       : JsonConvert.DeserializeObject<MdMetaData>(result.Fields[fieldName]);
        }
    }
}