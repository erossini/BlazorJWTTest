using System.Text.Json.Serialization;

namespace Conjoint.Shared.Models.Responses
{
    /// <summary>
    /// Class BaseDataset.
    /// </summary>
    public class BaseDataset
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        [JsonPropertyName("data")]
        public List<decimal>? Data { get; set; }
        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        [JsonPropertyName("backgroundColor")]
        public string? BackgroundColor { get; set; }
    }
}
