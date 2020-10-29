using System;
using BDS.Framework;
using BDS.Pipeline.News.FuYang;
using BDS.Pipeline.News.FuYang.GovernmentAnnouncement;

namespace BDS.Pipeline.News.FuYang.Run
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------Start Invoke--------------");
            Pipeline_GonvermentAnnouncement pipeline_GonvermentAnnouncement = new Pipeline_GonvermentAnnouncement();
            pipeline_GonvermentAnnouncement.Processor();
            Console.WriteLine("------Enter Key to end process.--------------");
            Console.ReadKey();
            
        }
    }
}
