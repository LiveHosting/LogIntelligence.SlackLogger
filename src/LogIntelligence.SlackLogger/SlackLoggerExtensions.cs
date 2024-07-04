using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace LogIntelligence.SlackLogger
{
    public static class SlackLoggerExtensions
    {
        public static ILoggingBuilder AddSlackLogger(this ILoggingBuilder builder, Action<SlackLoggerOptions> configureOptions)
        {
            builder.Services.Configure<SlackLoggerOptions>(configureOptions);
            builder.Services.AddSingleton<ILoggerProvider, SlackLoggerProvider>();
            //builder.Services.AddSingleton<ILoggerProvider>(serviceProvider =>
            //{
            //    var options = serviceProvider.GetRequiredService<IOptions<SlackLoggerOptions>>();
            //    var loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();
            //    return new SlackLoggerProvider(options, loggerFactory);
            //});
            return builder;
        }
    }
}