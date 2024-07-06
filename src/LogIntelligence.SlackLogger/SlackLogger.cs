using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace LogIntelligence.SlackLogger
{
    public class SlackLogger : ILogger
    {
        private readonly SlackLoggerOptions options;
        private static readonly HttpClient _httpClient = new();

        public SlackLogger(IOptions<SlackLoggerOptions> Options)
        {
            options = Options.Value ?? throw new ArgumentNullException(nameof(Options));
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return NoOpDisposable.Instance;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= options.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var message = formatter(state, exception);
            if (!string.IsNullOrEmpty(message))
            {
                // Pass additional details to SendMessageToSlackAsync
                _ = SendMessageToSlackAsync(
                    message: message,
                    logLevel: logLevel,
                    eventId: eventId,
                    exception: exception,
                    applicationName: options.ApplicationName,
                    environmentName: options.EnvironmentName,
                    hostname: Environment.MachineName
                ).ConfigureAwait(false);
            }
        }

        private async Task SendMessageToSlackAsync(string message, LogLevel logLevel, EventId eventId, Exception? exception, string applicationName, string environmentName, string hostname)
        {
            var color = GetColorForLogLevel(logLevel); // Determine the color based on the log level
            var exceptionDetails = exception != null ? $"\n*Exception:* {exception.Message}\nStackTrace: {exception.StackTrace}" : string.Empty;
            var formattedMessage = $"*{applicationName}* ({environmentName}) on {hostname}\n*Level:* {logLevel}\n*EventId:* {eventId}\n*Message:* {message}{exceptionDetails}";
            
            var payload = new
            {
                attachments = new[]
                {
                    new
                    {
                        color = color,
                        text = formattedMessage,
                        mrkdwn_in = new[] { "text" }
                    }
                }
            };

            var serializedPayload = JsonSerializer.Serialize(payload);
            var content = new StringContent(serializedPayload, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(options.WebHooksUrl, content).ConfigureAwait(false);
        }

        private string GetColorForLogLevel(LogLevel logLevel)
        {
            return logLevel switch
            {
                LogLevel.Trace => "#9d9d9d", // Grey
                LogLevel.Debug => "#9d9d9d", // Grey
                LogLevel.Information => "good", // Green
                LogLevel.Warning => "warning", // Yellow
                LogLevel.Error => "danger", // Red
                LogLevel.Critical => "#ff0000", // Brighter Red
                _ => "#9d9d9d", // Default to grey
            };
        }

        private class NoOpDisposable : IDisposable
        {
            public static NoOpDisposable Instance { get; } = new NoOpDisposable();

            public void Dispose() { }
        }
    }
}
