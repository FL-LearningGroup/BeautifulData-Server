using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class TestA
    {
        public string PropertyA1 { get; set; }
        public string PropertyA2 { get; set; }
    }

    public class TestB
    {
        public string PropertyB1 { get; set; }
        public string PropertyB2 { get; set; }
    }
    public class GenericT<T>
    {
        public void PrintfClassProperties()
        {
            PropertyInfo[] propertyInfos = typeof(T).GetProperties();
            foreach(var info in propertyInfos)
            {
                Console.WriteLine("Property Name: {0}", info.Name);
            }
        }
    }
    public class GenericT : IProcess
    {
        public void InvokeMain()
        {
            GenericT<TestA> testA = new GenericT<TestA>();
            testA.PrintfClassProperties();

            GenericT<TestB> testB = new GenericT<TestB>();
            testB.PrintfClassProperties();
        }
    }
}
