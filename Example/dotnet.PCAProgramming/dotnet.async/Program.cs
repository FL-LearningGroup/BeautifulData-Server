using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace dotnet.async
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ThreadSemaphore.Start();
            EndProcess();
        }

        static void EndProcess()
        {
            do
            {
                Console.WriteLine();
                Console.WriteLine("Please enter q/Q to end the process.");
            }
            while (Console.ReadKey().KeyChar.ToString().ToLower() != "q");
        }
    }
}
