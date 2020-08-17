namespace BDS.Pipeline.FuYang
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using log4net;
    using log4net.Config;
    using log4net.Repository;

    public static class Logger
    {
        private static ILog _log;
        private static void EnsureLogger()
        {
            if (_log != null) return;

            var assembly = Assembly.GetEntryAssembly();
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            var configFile = GetConfigFile();

            // Configure Log4Net
            XmlConfigurator.Configure(logRepository, configFile);
            _log = LogManager.GetLogger(assembly, assembly.ManifestModule.Name.Replace(".dll", "").Replace(".", " "));
        }
        private static FileInfo GetConfigFile()
        {
            FileInfo configFile = null;

            // Search config file
            var configFileNames = new[] { "log4net.config" };

            foreach (var configFileName in configFileNames)
            {
                configFile = new FileInfo(configFileName);

                if (configFile.Exists) break;
            }

            if (configFile == null || !configFile.Exists) throw new NullReferenceException("Log4net config file not found.");

            return configFile;
        }
        public static void Debug(string message)
        {
            EnsureLogger();

            _log.Debug(message);
        }
        public static void Info(string message)
        {
            EnsureLogger();

            _log.Debug(message);
        }
        public static void Error(string message)
        {
            EnsureLogger();

            _log.Error(message);
        }
        public static void Fatal(string message)
        {
            EnsureLogger();

            _log.Fatal(message);
        }
        public static void Warn(string message)
        {
            EnsureLogger();

            _log.Warn(message);
        }
    }
}
