namespace ExamineMd.Models
{
    using System;

    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// A virtual content page for displaying markdown.
    /// </summary>
    public class MarkdownVirtualContent : PublishedContentWrapped
    {
        
        private string _urlPath;

        private string _pageName;

        private string _pageTypeAlias;

        public MarkdownVirtualContent(IPublishedContent content, IMdFile md)
            : base(content)
        {
            Markdown = md;
        }

        internal MarkdownVirtualContent(IPublishedContent content)
            : base(content)
        {
        }

        public string PageTitle
        {
            get
            {
                return Markdown == null ? "ExamineMd" : Markdown.Title;
            }
        }

        public IMdFile Markdown { get; set; }

        #region IPublishedContent

        //public override string Url
        //{
        //    get { return base.Url.EnsureEndsWith('/') + (_urlPath ?? UrlName); }
        //}

        public override PublishedContentType ContentType
        {
            get { return null; }
        }

        public override IPublishedContent Parent
        {
            get { return Content; }
        }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public override int Id
        {
            get { return int.MaxValue; }
        }

        public override string Name
        {
            get { return _pageName; }
        }

        public override string UrlName
        {
            get { return _pageName.ToLowerInvariant(); }
        }

        public override string DocumentTypeAlias
        {
            get { return Constants.ExamineMdContentTypeAlias; }
        }

        public override int DocumentTypeId
        {
            get { return int.MaxValue; }
        }

        public override string Path
        {
            get { return Content.Path.EnsureEndsWith(',') + Id; }
        }

        public override int Level
        {
            get { return Content.Level + 1; }
        }

        #endregion
    }
}