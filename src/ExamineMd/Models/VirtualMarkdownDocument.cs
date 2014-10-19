namespace ExamineMd.Models
{
    using System;

    using Umbraco.Core.Models;

    using Constants = ExamineMd.Constants;

    /// <summary>
    /// A virtual content page for displaying markdown.
    /// </summary>
    public class VirtualMarkdownDocument : VirtualMarkdownBase, IVirtualMarkdownDocument
    {        
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMarkdownDocument"/> class.
        /// </summary>
        /// <param name="content">
        /// The content.
        /// </param>
        /// <param name="parent">
        /// The parent List.
        /// </param>
        /// <param name="md">
        /// The <see cref="IMdFile"/>
        /// </param>
        public VirtualMarkdownDocument(IPublishedContent content, IPublishedContent parent, IMdFile md)
            : base(content, parent, md.Path)
        {
            Mandate.ParameterNotNull(md, "md");

            Markdown = md;
        }

        /// <summary>
        /// Gets the relevance.
        /// </summary>
        public string Relevance
        {
            get
            {
                return string.IsNullOrEmpty(Markdown.MetaData.Relevance)
                    ? string.Empty
                    : Markdown.MetaData.Relevance;
            }
        }

        /// <summary>
        /// Gets the revision.
        /// </summary>
        public string Revision
        {
            get
            {
                return string.IsNullOrEmpty(Markdown.MetaData.Revision)
                    ? string.Empty
                    : Markdown.MetaData.Revision;
            }
        }

        /// <summary>
        /// Gets the <see cref="IMdFile"/> deserialized from the Examine index
        /// </summary>
        public IMdFile Markdown { get; private set; }

        #region IPublishedContent

        ///// <summary>
        ///// Gets the url.
        ///// </summary>
        ///// <remarks>
        ///// This is a virtual path not associated with an actual Umbraco content node. 
        ///// </remarks>
        //public override string Url
        //{
        //    get
        //    {
        //        var start = StartingPath.Value.EnsureForwardSlashes();
        //        var routeIndex = Content.Url.IndexOf(start, StringComparison.OrdinalIgnoreCase);
        //        if (routeIndex <= 0)
        //        {
        //            return Content.Url.EnsureNotEndsWith('/') + Markdown.SearchableUrl();
        //        }

        //        return this.Content.Url.Substring(0, this.Content.Url.IndexOf(start, StringComparison.OrdinalIgnoreCase) + start.Length).EnsureNotEndsWith('/') + this.Markdown.SearchableUrl();
        //    }
        //}

        /// <summary>
        /// Gets the id.
        /// </summary>
        /// <remarks>
        /// This is a virtual node so the Key does not correspond to Umbraco content
        /// </remarks>
        public override int Id
        {
            get { return int.MaxValue; }
        }

        /// <summary>
        /// Gets the Umbraco Content Name.
        /// </summary>
        public override string Name
        {
            get
            {
                return Markdown.Title;
            }
        }

        /// <summary>
        /// Gets the Umbraco UrlName.
        /// </summary>
        public override string UrlName
        {
            get
            {
                var lastIndex = Markdown.SearchableUrl().LastIndexOf('/') + 1;
                return Markdown.SearchableUrl().Substring(lastIndex, Markdown.SearchableUrl().Length - lastIndex);
            }
        }

        /// <summary>
        /// Gets the Umbraco DocumentTypeAlias.
        /// </summary>
        public override string DocumentTypeAlias
        {
            get { return Constants.ContentTypes.ExamineMdMarkdownDocument; }
        }

        #endregion

    }
}