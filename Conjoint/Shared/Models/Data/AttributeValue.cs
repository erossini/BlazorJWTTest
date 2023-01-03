namespace Conjoint.Shared.Models.Data
{
    /// <summary>
    /// Attribute Values
    /// </summary>
    public class AttributeValue
    {
        /// <summary>
        /// Gets or sets the attribute.
        /// </summary>
        /// <value>
        /// The attribute.
        /// </value>
        public string? Title { get; set; }
        /// <summary>
        /// Gets or sets the values.
        /// </summary>
        /// <value>
        /// The values.
        /// </value>
        public List<ValueText> Values { get; set; } = new List<ValueText>();
        /// <summary>
        /// Gets or sets the default value.
        /// </summary>
        /// <value>
        /// The default value.
        /// </value>
        public int DefaultValue { get; set; }
    }
}
