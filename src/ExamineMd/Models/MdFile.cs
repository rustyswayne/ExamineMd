namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Represents an MdFile
    /// </summary>
    public class MdFile : IMdFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdFile"/> class.
        /// </summary>
        public MdFile()
            : this(new MdMetaData())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MdFile"/> class.
        /// </summary>
        /// <param name="metaData">
        /// The meta data.
        /// </param>
        internal MdFile(IMdMetaData metaData)
        {
            Mandate.ParameterNotNull(metaData, "metaData");

            MetaData = metaData;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the file with respect to the file store root.
        /// </summary>
        public IMdPath Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the meta data data.
        /// </summary>
        public IMdMetaData MetaData { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        public DateTime DateCreated { get; set; }
    }
}