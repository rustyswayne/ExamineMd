namespace ExamineMd.Search
{
    /// <summary>
    /// Defines the MarkdownQuery object.
    /// </summary>
    public interface IMarkdownQuery
    {
        /// <summary>
        /// Gets the <see cref="IMdFileQuery"/>.
        /// </summary>
        IMdFileQuery Files { get; }

        /// <summary>
        /// Gets the <see cref="IMdPathQuery"/>.
        /// </summary>
        IMdPathQuery Paths { get; }
    }
}