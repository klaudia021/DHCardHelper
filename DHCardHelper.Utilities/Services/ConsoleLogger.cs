using Microsoft.Extensions.Logging;

namespace DHCardHelper.Utilities.Services
{
    public class ConsoleLogger : IMyLogger
    {
        private ILogger<ConsoleLogger> _logger;
        public ConsoleLogger(ILogger<ConsoleLogger> logger)
        {
            _logger = logger;
        }
        public void Info(string message)
        {
            _logger.LogInformation(Format("INFO", message));
        }
        public void Error(string message)
        {
            _logger.LogError(Format("ERROR", message));
        }
        private string Format(string type, string message)
        {
            return $"[{type}] [{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{message}]";
        }
    }
}
