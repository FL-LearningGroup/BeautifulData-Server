using System;
using BDS.Pipeline.FuYang;
using System.Collections.Generic;

namespace BDS.Pipeline.FuYang.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.Info("Call pipeline");
            Console.WriteLine("Start");
            Pipeline_FuYangNews.StartWork();
            //Console.WriteLine();
            Console.WriteLine("End");
            Console.ReadKey();
        }
    }
}
