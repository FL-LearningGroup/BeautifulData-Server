using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BDS.Runtime
{
    /// <summary>
    /// Load configuration information from xml file.
    /// </summary>
    internal class Config
    {
        /// <summary>
        /// Load pipline configuration information from xml file.
        /// </summary>
        /// <param name="xmlPath">pipeline configuration xml file path.</param>
        /// <returns>Resturn assembly configuration information.</returns>
        public static List<AssemblyConfig> LoadAssemblyConfig(string xmlPath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            List<AssemblyConfig> assemblyConfigs = new List<AssemblyConfig>();
            using(FileStream fs = new FileStream(xmlPath, FileMode.Open, FileAccess.Read))
            {
                xmlDoc.Load(fs);
            }
            string query = "//pipeline";
            XmlNodeList pipelineNodes = xmlDoc.SelectNodes(query);
            foreach(XmlNode node in pipelineNodes)
            {
                assemblyConfigs.Add(new AssemblyConfig() 
                    { AssemblyKey = node.Attributes["assemblyKey"].Value, AssemblyPath = node.Attributes["assemblyPath"].Value, AssemplyStatus = node.Attributes["assemblyStatus"].Value }
                );
            }

            return assemblyConfigs;
        }
    }
}
