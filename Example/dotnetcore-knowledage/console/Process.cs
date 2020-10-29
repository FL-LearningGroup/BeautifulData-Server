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
            AsyncAndTask asyncAndTask = new AsyncAndTask();
            asyncAndTask.AsyncTask();
            Console.WriteLine("Await");
            EndTag();
        }
    }
}
