namespace ExamineMd.Models
{
    using System;

    /// <summary>
    /// Represents an MdFile
    /// </summary>
    public class MdFile : MdEntity, IMdFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdFile"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public MdFile(string key)
            : this(key, new MdMetaData())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MdFile"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="metaData">
        /// The meta data.
        /// </param>
        internal MdFile(string key, IMdMetaData metaData)
            : base(key)
        {
            Mandate.ParameterNotNull(metaData, "metaData");

            MetaData = metaData;
        }


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