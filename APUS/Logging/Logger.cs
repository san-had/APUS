namespace APUS.Logging
{
    using log4net;
    using log4net.Config;
    using System.IO;
    using System.Reflection;

    public sealed class Logger
    {
        private static ILog logger;

        public int LogCounter { get; set; }

        private Logger()
        {
        }

        static Logger()
        {
            LoggerSetup();
        }

        public static void WriteLog(string message)
        {
            logger.Info(message);
        }

        private static void LoggerSetup()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));

            logger = LogManager.GetLogger(typeof(Logger));
        }
    }
}