using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using BDS.Runtime.Models;

namespace BDS.Runtime
{
    public static class ServerConfigOperation
    {
        public static void SubscriptionServerConfigWatcher()
        {
            ServerConfigWatcher.watcher.Changed += LoadServerConfigSwitch;
            ServerConfigWatcher.watcher.EnableRaisingEvents = true;
        }
        /// <summary>
        /// Load pipline configuration information from xml file.
        /// </summary>
        /// <param name="xmlPath">pipeline configuration xml file path.</param>
        /// <returns>Resturn assembly configuration information.</returns>
        public static List<PipelineAssemblyConfig> LoadAssemblyConfig(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<PipelineAssemblyConfig> assemblyConfigs = new List<PipelineAssemblyConfig>();
            if (IsFileLocked(xmlPath))
            {
                Logger.Error(String.Format("{0} has been open another program", xmlPath));
                return null;
            }
            using (FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                xmlDoc.Load(fs);
            }
            string query = "//pipelines//element";
            XmlNodeList pipelineNodes = xmlDoc.SelectNodes(query);
            foreach (XmlNode node in pipelineNodes)
            {
                assemblyConfigs.Add(new PipelineAssemblyConfig()
                { AssemblyKey = node.Attributes["assemblyKey"].Value, AssemblyPath = Path.Combine(GlobalVariables.WorkFolder,node.Attributes["assemblyPath"].Value), AssemblyStatus = node.Attributes["assemblyStatus"].Value, ScheduleTime = node.Attributes["scheduleTime"].Value }
                );
            }
            return assemblyConfigs;

        }
        public static void LoadServerConfigSwitch(object source, FileSystemEventArgs eventArgs)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<PipelineAssemblyConfig> assemblyConfigs = new List<PipelineAssemblyConfig>();
            if (!IsFileLocked(eventArgs.FullPath))
            {
                using(FileStream fileStream = new FileStream(eventArgs.FullPath, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(ServerConfig));
                    GlobalVariables.ServerConfig = (ServerConfig) xmlSerializer.Deserialize(fileStream);
                }
            }
        }
        /// <summary>
        /// Check if the file is locked by another program.
        /// </summary>
        /// <param name="xmlPath">File path</param>
        /// <returns>ture: lock, false: unlock</returns>
        public static bool IsFileLocked(string xmlPath)
        {
            try
            {
                using (FileStream stream = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
                {
                    stream.Close();
                }
            }
            catch (IOException ex)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                Logger.Error(String.Format("{0} has been open another program, exception message {1}", xmlPath, ex.Message + ex.InnerException));
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
