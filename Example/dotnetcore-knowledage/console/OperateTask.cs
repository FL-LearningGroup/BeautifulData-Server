using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.DotNetCoreKnowledage
{
    class OperateTask
    {
        static string status = "wait";
        static async Task CallTask()
        {
            status = "call task";
            Console.WriteLine("Invoke call task");
            await Task.Delay(6000);
            Console.WriteLine("End Invoke call task");

        }
        static async Task Main_Stop()
        {
            Process.StartTag();
            while(true)
            {
                var task = CallTask();
                await Task.Delay(2000);
            }
            Process.EndTag();
        }
    }
}
