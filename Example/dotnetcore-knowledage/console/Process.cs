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
            ObjectPassAsRefOrValue objectPassAsRefOrValue = new ObjectPassAsRefOrValue() { Value = 1, GSDateTime = DateTime.Now };
            objectPassAsRefOrValue.PrintMessage();
            SetObjectValue.SetValue(objectPassAsRefOrValue, 100, DateTime.Now.AddDays(1));
            objectPassAsRefOrValue.PrintMessage();
            EndTag();
        }
    }
}
