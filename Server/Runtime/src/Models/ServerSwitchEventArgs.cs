using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.Runtime.Models
{
    [Obsolete("no need", true)]
    public class ServerSwitchEventArgs : EventArgs
    {
        public ServerConfigSwitch Switch { get; set; }
    }
}
