using Microsoft.Extensions.Logging;

namespace LogIntelligence.SlackLogger
{
    /// <summary>
    /// Represents the options for configuring the Slack logger.
    /// </summary>
    public class SlackLoggerOptions
    {
        private string? webHooksUrl;
        private string? applicationName;
        private string? environmentName;

        /// <summary>
        /// Gets or sets the minimum log level.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Information;

        /// <summary>
        /// Gets or sets the Slack WebHooks URL.
        /// Throws InvalidOperationException if the value is null when getting.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the WebHooksUrl is null when getting.</exception>
        /// <exception cref="ArgumentException">Thrown if the WebHooksUrl is set to a null or empty string.</exception>
        public string WebHooksUrl
        {
            get => webHooksUrl ?? throw new InvalidOperationException("WebHooksUrl must not be null.");
            set => webHooksUrl = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("WebHooksUrl must not be null or empty.");
        }

        /// <summary>
        /// Gets or sets the name of the application using the Slack logger.
        /// Throws InvalidOperationException if the value is null when getting.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the ApplicationName is null when getting.</exception>
        /// <exception cref="ArgumentException">Thrown if the ApplicationName is set to a null or empty string.</exception>
        public string ApplicationName
        {
            get => applicationName ?? throw new InvalidOperationException("ApplicationName must not be null.");
            set => applicationName = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("ApplicationName must not be null or empty.");
        }

        /// <summary>
        /// Gets or sets the environment name where the Slack logger is used.
        /// Throws InvalidOperationException if the value is null when getting.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if the EnvironmentName is null when getting.</exception>
        /// <exception cref="ArgumentException">Thrown if the EnvironmentName is set to a null or empty string.</exception>
        public string EnvironmentName
        {
            get => environmentName ?? throw new InvalidOperationException("EnvironmentName must not be null.");
            set => environmentName = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("EnvironmentName must not be null or empty.");
        }
    }
}
