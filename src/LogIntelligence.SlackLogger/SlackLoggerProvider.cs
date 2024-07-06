using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogIntelligence.SlackLogger
{
    /// <summary>
    /// Provides an implementation of <see cref="ILoggerProvider"/> for creating instances of <see cref="SlackLogger"/>.
    /// </summary>
    public class SlackLoggerProvider : ILoggerProvider
    {
        private readonly IOptions<SlackLoggerOptions> options;
        private bool disposed = false; // For IDisposable implementation

        /// <summary>
        /// Initializes a new instance of the <see cref="SlackLoggerProvider"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the Slack logger.</param>
        public SlackLoggerProvider(IOptions<SlackLoggerOptions> options)
        {
            this.options = options;
        }

        /// <summary>
        /// Creates a new <see cref="SlackLogger"/> instance.
        /// </summary>
        /// <param name="categoryName">The category name for messages produced by the logger.</param>
        /// <returns>A <see cref="SlackLogger"/> instance.</returns>
        public ILogger CreateLogger(string categoryName)
        {
            // Optionally, use categoryName for logger customization
            return new SlackLogger(options);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Implement IDisposable correctly
            if (!disposed)
            {
                // Free any managed objects here if any
                disposed = true;
            }
        }
    }
}