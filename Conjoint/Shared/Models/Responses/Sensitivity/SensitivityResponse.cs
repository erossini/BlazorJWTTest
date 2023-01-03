using System.Text.Json.Serialization;

namespace Conjoint.Shared.Models.Responses.Sensitivity
{
    /// <summary>
    /// Class SensitivityResponse.
    /// Implements the <see cref="Conjoint.Shared.Models.Responses.BaseResponse" />
    /// </summary>
    /// <seealso cref="Conjoint.Shared.Models.Responses.BaseResponse" />
    public class SensitivityResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the labels.
        /// </summary>
        /// <value>The labels.</value>
        [JsonPropertyName("labels")]
        public List<string>? Labels { get; set; }

        /// <summary>
        /// Gets or sets the datasets.
        /// </summary>
        /// <value>The datasets.</value>
        [JsonPropertyName("datasets")]
        public List<SensitivityDataset>? Datasets { get; set; }
    }
}
