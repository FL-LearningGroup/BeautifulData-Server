using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class Split
    {
        static void Main_Stop()
        {
            Process.StartTag();
            string str = "adsd[:]asdsdad[:]";
            foreach(var item in str.Split("[:]"))
            {
                Console.WriteLine(item);
            }
            Process.EndTag();
        }
    }
}
