using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace LogIntelligence.SlackLogger.Tests
{
    [TestClass]
    public class SlackLoggerTests
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<SlackLoggerTests> logger;

        public TestContext? TestContext { get; set; }

        public SlackLoggerTests()
        {
            var services = new ServiceCollection();
            services.AddOptions();
            services.AddLogging();
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddSlackLogger(configure => 
                {
                    configure.LogLevel = LogLevel.Debug;
                    //configure.WebHooksUrl = "https://hooks.slack.com/services/your-webhook-url";
                    configure.WebHooksUrl = "https://hooks.slack.com/services/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";
                    configure.ApplicationName="SlackLoggerTests";
                    configure.EnvironmentName="Development";
                });
            });

            // Build the service provider
            serviceProvider = services.BuildServiceProvider();

            // Get the logger
            logger= serviceProvider.GetRequiredService<ILogger<SlackLoggerTests>>();
        }

        [TestMethod]
        public void TestServiceProviderInitialization()
        {
            Assert.IsNotNull(serviceProvider);
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public async Task TestLoggingAsync()
        {
            logger.LogTrace("This is a test trace message.");
            logger.LogDebug("This is a test debug message.");
            logger.LogInformation("This is a test information message.");
            logger.LogWarning("This is a test warning message.");
            logger.LogError("This is a test error message.");
            logger.LogCritical("This is a test critical message.");
            await Task.Delay(1000);
        }
    }
}