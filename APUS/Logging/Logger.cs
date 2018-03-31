namespace APUS.Logging
{
    using log4net;
    using log4net.Config;
    using System.IO;
    using System.Reflection;

    public sealed class Logger
    {
        private ILog logger;

        private static readonly Logger singletonLogger = new Logger();

        private Logger()
        {
            LoggerSetup();
        }

        public static Logger GetInstance()
        {
            return singletonLogger;
        }

        private void LoggerSetup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            logger = LogManager.GetLogger(typeof(Logger));
        }

        public void WriteLog(string message)
        {
            logger.Info(message);
        }
    }
}