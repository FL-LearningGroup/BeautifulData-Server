using System;
using System.Collections.Generic;
using System.Text;

namespace BDS.CollectData
{
    public interface ITransferResourceData
    {
        /// <summary>
        /// transfer data of resource.
        /// </summary>
        /// <param name="type">tansfer type(json,xml,database...)</param>
        bool TransferData(string type);
    }
}
