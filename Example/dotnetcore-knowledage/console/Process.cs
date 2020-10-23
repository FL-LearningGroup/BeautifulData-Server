using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public static partial class Process
    {
        public static void StartTag()
        {
            Console.WriteLine("---------Start process--------------------------------");
        }
        public static void EndTag()
        {
            Console.WriteLine("---------Please enter key to end the process----------");
            Console.ReadKey();
        }
        static void Main()
        {
            StartTag();
            List<OverrideEquls> overrideEquls = new List<OverrideEquls>();
            overrideEquls.Add(new OverrideEquls("First"));
            overrideEquls.Add(new OverrideEquls("Second"));
            overrideEquls.Add(new OverrideEquls("Third"));
            overrideEquls.Add(new OverrideEquls("Fourth"));
            DriverClass driverClass = new DriverClass("Second");
            var oper = overrideEquls.Remove(new OverrideEquls(driverClass));
            EndTag();
        }
    }
}
