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
            FuYangNewsPipeline.StartWork();
            //Console.WriteLine();
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
