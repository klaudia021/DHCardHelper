using System.Runtime.CompilerServices;

namespace DHCardHelper.Services
{
    public class ConsoleLogger : IMyLogger
    {
        private ILogger<ConsoleLogger> _logger;
        private readonly IWebHostEnvironment _env;
        public ConsoleLogger(IWebHostEnvironment env, ILogger<ConsoleLogger> logger)
        {
            _env = env;
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
