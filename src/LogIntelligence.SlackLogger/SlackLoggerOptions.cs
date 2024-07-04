using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogIntelligence.SlackLogger
{
    public class SlackLoggerOptions
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Information;
        public string WebHooksUrl { get; set; }
        public string ApplicationName { get; set; }
        public string EnvironmentName { get; set; }
    }
}
