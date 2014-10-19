namespace ExamineMd.Models
{
    using Umbraco.Core;

    /// <summary>
    /// Represents a MdPath.
    /// </summary>
    public class MdPath : IMdPath
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdPath"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public MdPath(string path)
            : this(SearchHelper.GetPathKey(path), path)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MdPath"/> class.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="path">
        /// The path.
        /// </param>
        public MdPath(string key, string path)
            : this(key, path, string.Empty)
        {           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MdPath"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public MdPath(string key, string path, string fileName)
        {
            this.Key = key;
            this.Value = path.IsNullOrWhiteSpace() ? "\\" : path.EnsureBackSlashes();
            this.FileName = fileName;
        }


        /// <summary>
        /// Gets the path Key.
        /// </summary>
        public string Key { get; private set; }
  

        /// <summary>
        /// Gets the path value.
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets a value indicating whether is document.
        /// </summary>
        public bool IsDocument
        {
            get
            {
                return !string.IsNullOrEmpty(FileName);
            }
        }
    }
}