using BDS.DataReport.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BDS.Pipeline.FuYang
{
    public static class Config
    {
        private static XmlDocument configDocXml = LoadConfigXml();
        public static XmlDocument LoadConfigXml()
        {
            string configFilePath = AssemblyInformation.LocationFolder + @"\config\config.xml";
            XmlDocument docXml = new XmlDocument();
            using (FileStream fs = new FileStream(configFilePath, FileMode.Open,FileAccess.Read))
            {
                docXml.Load(fs);
            }
            configDocXml = docXml;
            return docXml;
        }

        public static string SelectEmailHostInfo(string workSiteId, string emailType, string paramName)
        {
            string query = String.Format("//workSite[@id='{0}']/dataReport/email[@type='{1}']/host/{2}", workSiteId, emailType, paramName);
            string value = configDocXml.SelectSingleNode(query).FirstChild.Value;
            return value;
        }

        public static List<ContactPerson> SelectEmailContactPerson(string workSiteId, string emailType, string contactType)
        {
            List<ContactPerson> contacts = new List<ContactPerson>();
            string query = String.Format("//workSite[@id='{0}']/dataReport/email[@type='{1}']/{2}/element", workSiteId, emailType, contactType);
            XmlNodeList elementList = configDocXml.SelectNodes(query);
            // Warning: bug may exist 
            foreach (XmlNode element in elementList)
            {
                //var tmp = element.SelectSingleNode("/address");
                string[] contactArray = new string[2];
                UInt16 point = 0;
                foreach (XmlNode param in element)
                {
                    contactArray[point] = param.FirstChild.Value;
                    point++;
                }
                contacts.Add(new ContactPerson() { Email = contactArray[0], Name = contactArray[1] });
            }
            return contacts;
        }
        public static string SelectDataFactoyInfo(string workSiteId, string factoryType, string paramName)
        {
            string query = String.Format("//workSite[@id='{0}']/dataFactory/factory[@type='{1}']/{2}", workSiteId, factoryType, paramName);
            string value = configDocXml.SelectSingleNode(query).FirstChild.Value;
            return value;
        }
    }
}
