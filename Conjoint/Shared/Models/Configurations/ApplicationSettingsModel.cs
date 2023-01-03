namespace Conjoint.Shared.Models.Configurations
{
    /// <summary>
    /// Application Setting Model
    /// </summary>
    public class ApplicationSettingsModel
    {
        /// <summary>
        /// Gets or sets the application settings.
        /// </summary>
        /// <value>
        /// The application settings.
        /// </value>
        public Applicationsettings? ApplicationSettings { get; set; }

        /// <summary>
        /// Gets or sets the project number.
        /// </summary>
        /// <value>The project number.</value>
        public string? ProjectNumber { get; set; }

        /// <summary>
        /// Gets or sets the subscription key.
        /// </summary>
        /// <value>The subscription key.</value>
        public string? SubscriptionKey { get; set; }
    }
}