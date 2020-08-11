using System;
using System.Collections.Generic;
using System.Text;
using log4net;

namespace BDS.DotNetCoreKnowledage
{
    public class OperateLog4net
    {
        static void Main(string[] args)
        {
            Process.StartTag();

            ILog log = LogManager.GetLogger("mylog", "test");
            log.Info("This is my first log message");

            Process.EndTag();
        }
    }
}
