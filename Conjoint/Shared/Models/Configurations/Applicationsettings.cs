namespace Conjoint.Shared.Models.Configurations
{
    /// <summary>
    /// Application Settings
    /// </summary>
    public class Applicationsettings
    {
        /// <summary>
        /// Gets or sets the authorized urls.
        /// </summary>
        /// <value>
        /// The authorized urls.
        /// </value>
        public string[]? AuthorizedUrls { get; set; }

        /// <summary>
        /// Gets or sets the scopes.
        /// </summary>
        /// <value>
        /// The scopes.
        /// </value>
        public string[]? Scopes { get; set; }
    }
}
