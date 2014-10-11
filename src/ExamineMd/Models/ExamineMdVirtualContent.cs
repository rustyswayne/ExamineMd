namespace ExamineMd.Models
{
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// A virtual content page for displaying markdown.
    /// </summary>
    public class ExamineMdVirtualContent : PublishedContentWrapped
    {
        /// <summary>
        /// The <see cref="IMdFile"/>.
        /// </summary>
        private IMdFile _md;

        private string _urlPath;

        private string _pageName;

        private string _pageTypeAlias;

        public ExamineMdVirtualContent(IPublishedContent content, MdFile md)
            : base(content)
        {

        }

        internal ExamineMdVirtualContent(IPublishedContent content)
            : base(content)
        {
        }

        public override string Url
        {
            get { return base.Url.EnsureEndsWith('/') + (_urlPath ?? UrlName); }
        }

        public override PublishedContentType ContentType
        {
            get { return null; }
        }

        public override IPublishedContent Parent
        {
            get { return Content; }
        }

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
    }
}