using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
        static async Task Main()
        {
            StartTag();
            EndTag();
        }
    }
}
