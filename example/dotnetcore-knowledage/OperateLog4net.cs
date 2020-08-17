using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using log4net;
using log4net.Config;
using log4net.Repository;

namespace BDS.DotNetCoreKnowledage
{
    /// <Reference>
    /// Blog: https://dev.mercymainb.tw/2018/10/19/dotnetcore-log4net/
    /// </Reference>
    public static class PipelineLogger
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

            _log.Debug($"Test log: {message}");
        }
        public static void Info(string message)
        {
            EnsureLogger();

            _log.Debug($"Test log: {message}");
        }
    }
    public class OperateLog4net
    {
        static void Main(string[] args)
        {
            Process.StartTag();
            // Load configuration
            //private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            //var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            //XmlConfigurator.Configure(logRepository, new FileInfo("log4net.config"));
            // log.Warn("Warn!");
            // Log some things
            PipelineLogger.Info("Hello logging world!");
            PipelineLogger.Debug("Error!");
            Process.EndTag();
        }
    }
}
