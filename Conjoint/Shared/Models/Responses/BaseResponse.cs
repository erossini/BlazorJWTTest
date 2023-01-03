using System.Text.Json.Serialization;

namespace Conjoint.Shared.Models.Responses
{
    /// <summary>
    /// Base Response
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="BaseResponse"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        [JsonIgnore]
        public bool Success { get; set; } = true;
    }
}
