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
            return new MdFile()
            {
                FileName = result.FieldAsString("fileName"),
                Path = result.FieldAsString("path"),
                Title = result.FieldAsString("title"),
                Body = result.FieldAsString("body"),
                MetaData = result.FieldAsMetaItemCollection(),
                DateCreated = result.FieldAsDateTime("createDate")
            };
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
        private static IEnumerable<IMdMetaItem> FieldAsMetaItemCollection(this SearchResult result, string fieldName = "metaData")
        {
            return !result.Fields.ContainsKey(fieldName)
                       ? new MdMetaItem[] { }
                       : JsonConvert.DeserializeObject<IEnumerable<MdMetaItem>>(result.Fields[fieldName]);
        }
    }
}