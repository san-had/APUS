namespace APUS.Utils
{
    using log4net;
    using log4net.Config;
    using System.IO;
    using System.Reflection;

    public class Logger : ILogger
    {
        private ILog logger;

        public int LogCounter { get; set; }

        public Logger()
        {
            LoggerSetup();
        }

        public void Log(string message)
        {
            this.logger.Info(message);
        }

        public void IncrementCounter()
        {
            LogCounter++;
        }

        private void LoggerSetup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            this.logger = LogManager.GetLogger(typeof(Logger));
        }
    }
}