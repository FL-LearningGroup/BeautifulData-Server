using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class ThrowExceptionCls
    {
        public List<string> list;
        // Test 01: Nested exception
        public static void NestedException01()
        {
            throw new Exception("exception 01");
        }
        public static void NestedException02()
        {
            NestedException01();
            throw new Exception("exception 02");
        }

        static void Main_Stop(string[] args)
        {
            Process.StartTag();
            try
            {
                NestedException02();
            }
            catch(Exception ex)
            {
                Console.WriteLine("exception: {0}", ex.Message);
            }
            Process.EndTag();
        }
    }
}
