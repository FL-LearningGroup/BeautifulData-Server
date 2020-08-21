using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace BDS.DotNetCoreKnowledage
{
    class ReadXML
    {
        static void Main()
        {
            XmlDocument xmlDoc = new XmlDocument();
            using(FileStream fs = new FileStream(@"readXml.xml", FileMode.Open, FileAccess.Read))
            {
                xmlDoc.Load(fs);
                //var id = xmlDoc.GetElementById("WorkSite01");
                var xmlElement = xmlDoc.GetElementsByTagName("host");
            }
        }
    }
}
