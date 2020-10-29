using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BDS.DotNetCoreKnowledage
{
    public class AsyncAndTask
    {
        public Task CallTask()
        {
            return Task.Run(() => {
                    Console.WriteLine("Invoke Task.");
                    int i = 0;
                    while ((i++) < 3000) ;
                }
            );
        }

        public async Task AsyncTask()
        {
            await Task.Delay(3000);
            Console.WriteLine("Invoke Async Task");
        }
    }
}
