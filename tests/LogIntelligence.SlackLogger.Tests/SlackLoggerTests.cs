using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;

namespace LogIntelligence.SlackLogger.Tests
{
    [TestClass]
    public class SlackLoggerTests
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<SlackLoggerTests> logger;

        public SlackLoggerTests()
        {
            var services = new ServiceCollection();
            //services.AddOptions();
            
            services.AddLogging(configure =>
            {
                configure.ClearProviders();
                configure.AddSlackLogger(configure => 
                {
                    configure.LogLevel = LogLevel.Trace;
                    //configure.WebHooksUrl = "https://hooks.slack.com/services/your-webhook-url";
                    configure.ApplicationName="Your App Name";
                    configure.EnvironmentName= "Development";
                });
            });

            // Build the service provider
            serviceProvider = services.BuildServiceProvider();

            // Get and store the logger
            logger= serviceProvider.GetRequiredService<ILogger<SlackLoggerTests>>();
        }

        [TestMethod]
        public void TestServiceProviderInitialization()
        {
            Assert.IsNotNull(serviceProvider);
            Assert.IsNotNull(logger);
        }

        [TestMethod]
        public async Task TestAllLogMessagesAsync()
        {
            Assert.IsNotNull(logger);
            logger.LogTrace("This is a test trace message.");
            logger.LogDebug("This is a test debug message.");
            logger.LogInformation("This is a test information message.");
            logger.LogWarning("This is a test warning message.");
            logger.LogError("This is a test error message.");
            logger.LogCritical("This is a test critical message.");
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestLogTraceMessage()
        {
            logger.LogTrace("This is a test trace message.");
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestLogDebugMessage()
        {
            logger.LogDebug("This is a test debug message.");
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestLogInformationMessage()
        {
            logger.LogInformation("This is a test information message.");
            await Task.Delay(1000);
        }

        [TestMethod]
        public async Task TestLogWarningMessage()
        {
            logger.LogWarning("This is a test warning message.");
            await Task.Delay(1000);
        }
    }
}