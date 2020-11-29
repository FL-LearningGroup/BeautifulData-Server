using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.DotNetCoreKnowledage
{
    public class ForeachOperation
    {
        public int Number { get; set; }
        public string msg { get; set; }
    }

/* 
Test:
        List<ForeachOperation> foreachOperationsList = new List<ForeachOperation>()
        {
            new ForeachOperation{ Number= 1, msg ="key1"  },
            new ForeachOperation{ Number= 2, msg ="key2"  }
        };
        foreachOperationsList.ForEach(item => item.msg = "key");
        foreachOperationsList.ForEach(item => Console.WriteLine(item.msg));

Result:
---------Start process--------------------------------
key
key
---------Please enter key to end the process----------
    */
}
