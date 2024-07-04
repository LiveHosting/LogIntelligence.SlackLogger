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
            return logLevel != LogLevel.None;
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
                SendMessageToSlackAsync(message).ConfigureAwait(false);
            }
        }

        private async Task SendMessageToSlackAsync(string message)
        {
            var payload = new StringContent(JsonSerializer.Serialize(new { text = message }), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(options.WebHooksUrl, payload).ConfigureAwait(false);
        }

        private class NoOpDisposable : IDisposable
        {
            public static NoOpDisposable Instance { get; } = new NoOpDisposable();

            public void Dispose() { }
        }
    }
}
