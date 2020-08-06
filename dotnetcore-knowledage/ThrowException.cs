using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class ThrowExceptionCls
    {
        public List<string> list;
        public static void ThrowExecption01()
        {
            Console.WriteLine("Before throw01");
            throw new Exception("throw01");
            Console.WriteLine("After throw01"); // The instruction not executed
        }
        public void ThrowExecption02(string str)
        {
            try
            {
                this.list.Add(str);
            }
            catch(Exception ex)
            {
                Console.WriteLine("ThrowExecption02: {0}", ex.Message);
                throw new Exception("Throw Execption 02");
            }
        }
        public void ThrowExecption03(string str)
        {
            throw new Exception("Throw Execption 03");
        }
        static void Main_Stop(string[] args)
        {
            Console.WriteLine("Stard run");
            ThrowExceptionCls exceptionCls = new ThrowExceptionCls();
            Console.WriteLine("End run");
            Console.ReadKey();
        }
    }
}
