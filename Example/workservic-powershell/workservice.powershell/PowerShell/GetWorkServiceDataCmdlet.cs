using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using workservice.powershell.Model;

namespace workservice.powershell.PowerShell
{
    [Cmdlet(VerbsCommon.Get, "WorkServiceData")]
    public class GetWorkServiceDataCmdlet: Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string name;
        protected override void ProcessRecord()
        {
            //DataMemory.Data.ForEach(d => WriteObject(String.Format("Order {0}, Message {1}" ,d.Order, d.Message)));
            WriteObject("Hello " + name + "!");
        }
    }
}
