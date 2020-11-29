using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BDS.Runtime.Models
{
    [XmlRoot(ElementName = "ServerConfig")]
    public class ServerConfig
    {
        [XmlElement(ElementName = "ControlSwitch")]
        public ServerConfigSwitch Switch { get; set; }
        public ServerConfig()
        {
            Switch = new ServerConfigSwitch();
        }
    }
}
