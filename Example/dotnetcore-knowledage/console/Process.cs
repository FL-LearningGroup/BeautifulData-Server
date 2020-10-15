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
            StringBuilder stringBuilder = new StringBuilder(50);
            stringBuilder.Append(@"<tr></tr>");
            stringBuilder.Append("asddas", 3, "asddas".Length);
            Console.WriteLine(stringBuilder.ToString());
            EndTag();
        }
    }
}
