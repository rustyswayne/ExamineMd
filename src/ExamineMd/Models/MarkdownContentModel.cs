namespace ExamineMd.Models
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web.Models;

    /// <summary>
    /// The markdown content model.
    /// </summary>
    public class MarkdownContentModel : PublishedContentWrapped
    {

        public MarkdownContentModel(IPublishedContent content)
            : base(content)
        {
        }

        public MarkdownContentModel Root { get; set; }

        public string Key { get; set; }

        public string NiceUrl { get; set; }

        public string FileName { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public IMdFileMetaData MetaData { get; set; }

        public DateTime DateCreated { get; set; }
    }
}