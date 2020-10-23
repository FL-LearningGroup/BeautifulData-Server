using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using BDS.Runtime.Models;

namespace BDS.Runtime
{
    public static class ServerConfig
    {
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
                { AssemblyKey = node.Attributes["assemblyKey"].Value, AssemblyPath = node.Attributes["assemblyPath"].Value, AssemblyStatus = node.Attributes["assemblyStatus"].Value, ScheduleTime = node.Attributes["scheduleTime"].Value }
                );
            }
            return assemblyConfigs;

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
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
    }
}
