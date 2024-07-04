using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace LogIntelligence.SlackLogger
{
    public class SlackLoggerProvider : ILoggerProvider
    {
        private readonly IOptions<SlackLoggerOptions> options;

        public SlackLoggerProvider(IOptions<SlackLoggerOptions> Options)
        {
            options=Options;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new SlackLogger(options);
        }

        public void Dispose() { }
    }
}