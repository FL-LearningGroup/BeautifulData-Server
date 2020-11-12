using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class ObjectPassAsRefOrValue
    {
        public int Value { get; set; }
        public DateTime GSDateTime { get; set; }

        public void PrintMessage()
        {
            Console.WriteLine(String.Format("Value: {0}, DateTime: {1}", Value, GSDateTime.ToString("yyyy-MM-dd HH:mm:ss")));
        }
    }

    public static class SetObjectValue
    {
        public static void SetValue(ObjectPassAsRefOrValue objectPassAsRefOrValue, int value, DateTime dt)
        {
            objectPassAsRefOrValue.Value = value;
            objectPassAsRefOrValue.GSDateTime = dt;
        }
    }

    #region Test 
    /** Code
        ObjectPassAsRefOrValue objectPassAsRefOrValue = new ObjectPassAsRefOrValue() { Value = 1, GSDateTime = DateTime.Now };
        objectPassAsRefOrValue.PrintMessage();
        SetObjectValue.SetValue(objectPassAsRefOrValue, 100, DateTime.Now.AddDays(1));
        objectPassAsRefOrValue.PrintMessage();
    */
    /** Result
        Value: 1, DateTime: 2020-11-12 15:35:57
        Value: 100, DateTime: 2020-11-13 15:35:57
     */
    #endregion
}
