using System;
using BDS.Pipeline.News.FuYang;

namespace BDS.Pipeline.News.FuYang.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------Start Invoke--------------");
            Pipeline_GonvermentAnnouncement.StartWork();

            Console.WriteLine("------Enter Key to end process.--------------");
            Console.ReadKey();
            
        }
    }
}
