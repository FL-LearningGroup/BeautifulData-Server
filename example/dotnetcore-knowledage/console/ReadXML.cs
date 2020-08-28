using BDS.DataReport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace BDS.DotNetCoreKnowledage
{
    class ReadXML
    {
        public static class Config
        {
            private static XmlDocument configDocXml= LoadConfigXml();
            public static XmlDocument LoadConfigXml()
            {
                XmlDocument docXml = new XmlDocument();
                //string configFilePath = AssemblyInformation.LocationFolder + @"\config\config.xml";
                string configFilePath = @"readXml.xml";
                using (FileStream fs = new FileStream(configFilePath, FileMode.Open, FileAccess.Read))
                {
                    docXml.Load(fs);
                }
                configDocXml = docXml;
                return docXml;
            }

            public static string SelectEmailHostInfo(string workSiteId, string emailType, string paramName)
            {
                string query = String.Format("//workSite[@id='{0}']/dataReport/email[@type='{1}']/host/{2}", workSiteId, emailType, paramName);
                XmlNode node = configDocXml.SelectSingleNode(query);
                var value = node.Attributes["test1"].Value;
                string value1 = configDocXml.SelectSingleNode(query).FirstChild.Value;
                return value1;
            }

            public static List<ContactPerson> SelectEmailContactPerson(string workSiteId, string emailType, string contactType)
            {
                List<ContactPerson> contacts =  new List<ContactPerson>();
                string query = String.Format("//workSite[@id='{0}']/dataReport/email[@type='{1}']/{2}/element", workSiteId, emailType, contactType);
                XmlNodeList elementList = configDocXml.SelectNodes(query);
                // Warning: bug may exist 
                foreach (XmlNode element in elementList)
                {
                    //var tmp = element.SelectSingleNode("/address");
                    string[] contactArray = new string[2];
                    UInt16 point = 0;
                    foreach(XmlNode param in element)
                    {
                        contactArray[point] = param.FirstChild.Value;
                        point++;
                    }
                    contacts.Add(new ContactPerson() { Email = contactArray[0], Name = contactArray[1] });
                }
                return contacts;
            }
        }
        public static void UpdateXml()
        {
            XmlDocument xmlDocument = new XmlDocument();
            using(FileStream fs = new FileStream("PipelineConfig.xml",FileMode.Open, FileAccess.Read))
            {
                xmlDocument.Load(fs);
            }
            XmlNodeList pipeline = xmlDocument.GetElementsByTagName("pipeline");

        }
        static void Main_Stop()
        {
            Process.StartTag();
            var value = Config.SelectEmailHostInfo("WorkSite01", "outlook", "address"); //Pass
            //var element = Config.SelectEmailContactPerson("WorkSite01", "outlook", "fromPerson");
            Process.EndTag();
        }
    }
}
