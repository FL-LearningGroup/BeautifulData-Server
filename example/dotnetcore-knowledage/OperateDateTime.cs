using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    class OperateDateTime
    {
        string fixDt = "F:Y0:M8:W0:D24:h0:m0:s0";
        string everyCycleDt = "EC:Y0:M0:W0:D1:h8:m0:s0";
        string apartCycleDt = "AC:Y0:M0:W0:D0:h0:m0:s30";
        private DateTime _executeTime;
        private DateTime _lastExecuteTime;
        private DateTime _nextExecuteTime;
        string dateFormat = "yyyy/MM/dd hh:mm:ss.fffffff";

        static void Execute(string msg)
        {
            Console.WriteLine(msg);
        }
        static void Main()
        {
            //Format: prefix:Y:M:W:D:h:m:s
            Process.StartTag();
            var list = new List<int>() { 10, 20, 30 };
            // Try to remove an element in a foreach list.
            foreach (int value in list)
            {
                Console.WriteLine("ELEMENT: {0}", value);
                list.Remove(value);
            }
            Process.EndTag();
        }
    }
}
