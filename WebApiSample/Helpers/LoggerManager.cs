using NLog;

namespace WebApiSample.Helpers
{
    public class LoggerManager : ILoggerManager
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }
    }
}
