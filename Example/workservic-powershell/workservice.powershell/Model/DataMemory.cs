using System;
using System.Collections.Generic;
using System.Text;

namespace workservice.powershell.Model
{
    static class DataMemory
    {
        public static List<ObjectData> Data { get; set; }
        static DataMemory()
        {
            Data = new List<ObjectData>();
        }
    }
}
