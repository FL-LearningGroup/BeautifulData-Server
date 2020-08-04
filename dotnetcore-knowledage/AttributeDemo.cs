#define TRACK_ON
using System;
using System.Diagnostics;

namespace BDS.DotNetCoreKnowledage
{
    public class Myclass
    {
        [Conditional("TRACK_ON")]
        public static void Message(string msg)
        {
            Console.WriteLine(msg);
        }
    }
    class AttributeDemo
    {
        static void function1()
        {
            Myclass.Message("In Function 1.");
            function2();
        }
        static void function2()
        {
            Myclass.Message("In Function 2.");
        }
        static void Main(string[] args)
        {
            Myclass.Message("In Main function.");
            function1();
            Console.ReadKey();
        }
    }
}
