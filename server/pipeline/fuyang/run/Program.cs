using System;
using BDS.Pipeline.FuYang;
using System.Collections.Generic;

namespace BDS.Pipeline.FuYang.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start");
            Console.WriteLine(FuYangNewsPipeline.StartWork());
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
