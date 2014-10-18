namespace ExamineMd.Models
{
    /// <summary>
    /// Represents and MdEntity.
    /// </summary>
    public abstract class MdEntity : IMdEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MdEntity"/> class.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        protected MdEntity(string key)
        {
            Mandate.ParameterNotNullOrEmpty(key, "key");

            Key = key;
        }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the relative path to the file with respect to the file store root.
        /// </summary>
        public IMdPath Path { get; set; }
    }
}