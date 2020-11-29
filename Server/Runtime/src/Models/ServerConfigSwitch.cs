using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BDS.Runtime.Models
{
    public class ServerConfigSwitch
    {
        private string _reloadDataBase;
        public event EventHandler ServerSwitchEvent;

        [XmlAttribute("ReloadDataBase")]
        public string ReloadDataBase 
        {
            get => _reloadDataBase;
            set
            {
                _reloadDataBase = value;
                if (ServerSwitchEvent != null && _reloadDataBase.ToUpper() == "ON")
                {
                    ServerSwitchEvent(this, new EventArgs());
                }
            }
        }
    }
}
